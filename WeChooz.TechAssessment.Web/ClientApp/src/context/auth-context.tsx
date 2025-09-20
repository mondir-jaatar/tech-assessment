import {Claim} from "../../api/auth/auth-service.tsx";
import {createContext, ReactNode, useContext, useEffect, useState} from "react";

type AuthContextType = {
    claims: Claim[] | null;
    setClaims: (claims: Claim[] | null) => void;
    logout: () => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
    const [claims, setClaimsState] = useState<Claim[] | null>(null);

    useEffect(() => {
        const stored = localStorage.getItem("claims");
        if (stored) {
            try {
                setClaimsState(JSON.parse(stored));
            } catch {
                localStorage.removeItem("claims");
            }
        }
    }, []);

    const setClaims = (newClaims: Claim[] | null) => {
        setClaimsState(newClaims);
        if (newClaims) {
            localStorage.setItem("claims", JSON.stringify(newClaims));
        } else {
            localStorage.removeItem("claims");
        }
    };

    // TODO
    const logout = () => {
        setClaims(null);
    };

    return (
        <AuthContext.Provider value={{ claims, setClaims, logout }}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error("useAuth must be used within AuthProvider");
    }
    return context;
}