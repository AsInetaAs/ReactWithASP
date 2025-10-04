import useSWR from 'swr';
import { getApi } from '@/api';
import { IDashboard } from '@/interfaces/IDashboard';
import { UsersTable } from './components/usersTable';


type AppUser = { id: string; userName?: string; email?: string; role?: string };

export default function Dashboard() {
    const { data, error, isLoading } = useSWR<IDashboard | undefined>(
        "admin/dashboard",
        getApi,
        { revalidateOnReconnect: true }
    );

    const {
        data: users,
        error: usersError
    } = useSWR<AppUser[] | undefined>(
        "admin/dashboard/users",   
        getApi,
        { revalidateOnReconnect: true }
    );

    return (
        <div>
            <h1 className='text-xl text-blue-950'>Admin Dashboard</h1>
            {error ? <div>{String(error)}</div> : null}
            {data?.text}

            <h2 className='text-lg mt-6 mb-2'>Vartotojai (AspNetUsers)</h2>
            <UsersTable users={users} error={usersError} />
        </div>
    );
}