

type AppUser = { id: string; userName?: string; email?: string; role?: string };

type Props = {
    users?: AppUser[] | undefined;
    error?: unknown;
};

export function UsersTable({ users, error }: Props) {
    if (error) return <div className="text-red-600">{String(error)}</div>;

    const list = Array.isArray(users) ? users : [];

    return (
        <div className="overflow-x-auto">
            <table className="min-w-full border">
                <thead>
                    <tr className="bg-gray-100">
                        <th className="border px-2 py-1 text-left">Id</th>
                        <th className="border px-2 py-1 text-left">UserName</th>
                        <th className="border px-2 py-1 text-left">Email</th>
                        <th className="border px-2 py-1 text-left">Role</th>
                    </tr>
                </thead>
                <tbody>
                    {list.map(u => (
                        <tr key={u.id}>
                            <td className="border px-2 py-1">{u.id}</td>
                            <td className="border px-2 py-1">{u.userName ?? ""}</td>
                            <td className="border px-2 py-1">{u.email ?? ""}</td>
                            <td className="border px-2 py-1">{u.role ?? ""}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}