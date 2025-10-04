import { Navigate } from "react-router-dom";
import { useStore, useShallow } from "@/store";
import type { ReactElement } from "react";


//export function ProtectedRoute({ children }: { children: JSX.Element }) {
export function ProtectedRoute({ children }: { children: ReactElement }) {
    const auth = useStore(useShallow((state) => state.auth));

    if (!auth) return null;
    if (!auth?.isAuthenticated) {
        return <Navigate to="/auth/signin" />;
    }

    return children;
}
