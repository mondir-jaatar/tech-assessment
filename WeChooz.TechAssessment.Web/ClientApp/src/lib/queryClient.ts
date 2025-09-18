import { QueryClient, QueryFunction } from "@tanstack/react-query";

async function throwIfResNotOk(res: Response) {
  if (!res.ok) {
    let errorText;
    try {
      // Try to get response text, but handle potential JSON responses too
      const contentType = res.headers.get('content-type');
      if (contentType && contentType.includes('application/json')) {
        const errorJson = await res.json();
        errorText = errorJson.message || JSON.stringify(errorJson);
      } else {
        errorText = await res.text();
      }
    } catch (error) {
      // If we can't parse the error response, fall back to status text
      errorText = res.statusText;
    }
    
    throw new Error(`${res.status}: ${errorText}`);
  }
}

// Helper function to ensure the URL is absolute and relative to the current window location
function resolveApiUrl(url: string): string {
  // If it's already an absolute URL but on another domain, convert it to a relative URL
  // This handles the case where we have a hard-coded Replit domain that won't work on IP deployment
  if (url.startsWith('http://') || url.startsWith('https://')) {
    // Parse the URL to extract the path
    try {
      const urlObj = new URL(url);
      
      // Check if it's the same origin - if so, we can keep the full URL
      if (urlObj.origin === window.location.origin) {
        return url;
      }
      
      // Replace the URL with just the pathname (making it relative to current domain)
      url = urlObj.pathname + urlObj.search + urlObj.hash;
      console.log(`Converted absolute URL to path: ${url}`);
    } catch (e) {
      console.error('Failed to parse URL:', url, e);
    }
  }
  
  // Get the base URL from window.location - this will be correct regardless of deployment
  const baseUrl = window.location.origin;
  
  // If it's an absolute path starting with /api, prefix with the base URL
  if (url.startsWith('/api/')) {
    return `${baseUrl}${url}`;
  }
  
  // If it's another absolute path, prefix with the base URL
  if (url.startsWith('/')) {
    return `${baseUrl}${url}`;
  }
  
  // If it's a relative API path, ensure it has /api prefix
  if (!url.startsWith('api/') && !url.includes('/api/')) {
    return `${baseUrl}/api/${url}`;
  }
  
  // Otherwise, assume it's a relative path and prefix with origin
  return `${baseUrl}/${url}`;
}

interface ApiRequestOptions {
  method?: string;
  body?: string;
  on401?: UnauthorizedBehavior;
  headers?: Record<string, string>;
}

export async function apiRequest(
  methodOrUrl: string,
  urlOrData: string | any = {},
  data?: any
): Promise<any> {
  let method: string;
  let url: string;
  let body: string | undefined;
  let on401: UnauthorizedBehavior = 'throw';
  let headers: Record<string, string> = {};
  
  // Handle both calling styles:
  // 1. apiRequest(url, options)
  // 2. apiRequest(method, url, data)
  if (urlOrData && typeof urlOrData === 'object') {
    // First style: apiRequest(url, options)
    method = 'GET';
    url = methodOrUrl;
    const options = urlOrData as ApiRequestOptions;
    method = options.method || 'GET';
    body = options.body;
    on401 = options.on401 || 'throw';
    headers = options.headers || {};
  } else {
    // Second style: apiRequest(method, url, data)
    method = methodOrUrl;
    url = urlOrData as string;
    body = data ? JSON.stringify(data) : undefined;
  }
  
  // Ensure the URL is absolute
  const resolvedUrl = resolveApiUrl(url);
  
  // Add a log to help debug in production
  console.log(`API Request: ${method} ${resolvedUrl}`);
  
  const fetchOptions: RequestInit = {
    method,
    headers: body 
      ? { "Content-Type": "application/json", ...headers } 
      : headers,
    body,
    credentials: "include",
  };

  try {
    const res = await fetch(resolvedUrl, fetchOptions);

    if (on401 === "returnNull" && res.status === 401) {
      return null;
    }

    await throwIfResNotOk(res);
    
    // Parse JSON if there's content to parse
    const contentType = res.headers.get('content-type');
    if (contentType && contentType.includes('application/json')) {
      return await res.json();
    }
    
    return res;
  } catch (error) {
    console.error(`API Request Error: ${method} ${resolvedUrl}`, error);
    throw error;
  }
}

type UnauthorizedBehavior = "returnNull" | "throw";
export const getQueryFn: <T>(options: {
  on401: UnauthorizedBehavior;
}) => QueryFunction<T> =
  ({ on401: unauthorizedBehavior }) =>
  async ({ queryKey }) => {
    try {
      // Ensure the URL is absolute
      const url = queryKey[0] as string;
      const resolvedUrl = resolveApiUrl(url);
      
      // Add a log to help debug in production
      console.log(`Query Request: GET ${resolvedUrl}`);
      
      const res = await fetch(resolvedUrl, {
        credentials: "include",
      });

      if (unauthorizedBehavior === "returnNull" && res.status === 401) {
        return null;
      }

      await throwIfResNotOk(res);
      
      // Try to parse the response as JSON
      try {
        return await res.json();
      } catch (jsonError) {
        // If we can't parse JSON, just return an empty object
        console.warn(`Could not parse JSON response from ${resolvedUrl}`, jsonError);
        return {};
      }
    } catch (error) {
      console.error(`Query Error: ${queryKey[0]}`, error);
      throw error;
    }
  };

export const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      queryFn: getQueryFn({ on401: "throw" }),
      refetchInterval: false,
      refetchOnWindowFocus: false,
      staleTime: Infinity,
      retry: false,
    },
    mutations: {
      retry: false,
    },
  },
});
