import {useAuth} from "../../../context/auth-context.tsx";
import {useMutation} from "@tanstack/react-query";
import {AuthenticationService, Claim} from "../../../../api/auth/auth-service.tsx";
import {PerformLoginRequest} from "../../../../api/auth/queries/perform-login-request.tsx";

export function useLogin() {
    const { setClaims } = useAuth();

    return useMutation<Claim[], Error, PerformLoginRequest>({
        mutationFn: (request: PerformLoginRequest) => AuthenticationService.Login(request),
        onSuccess: (claims) => {
            setClaims(claims);
        },
    });
}