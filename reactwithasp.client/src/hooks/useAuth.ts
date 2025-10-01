{/* import { useStore, useShallow } from "@/store";
import { useNavigate } from "react-router"; 
import { postApi } from "@api"; 

export function useAuth(): { logoutHandler: () => Promise<void>, auth: IAuth | undefined } {
    const navigate: NavigateFunction = useNavigate();
    const [setAuth, auth] = useStore(useShallow((state: IStore) => ({ setAuth: state.setAuth, auth: state.auth })));

    const logoutHandler = async (): Promise<void> => {
        await postApi('authentication/logout', {});
        setAuth(undefined);
        navigate('/auth/signin'); // Redirect to the login page
    };

    return { logoutHandler, auth };
}
*/}