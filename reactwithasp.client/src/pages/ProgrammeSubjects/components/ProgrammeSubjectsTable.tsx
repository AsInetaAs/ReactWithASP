import { ISubject } from "../../../interfaces/ISubject";

interface Props {
    attached: ISubject[];
    programmeId?: number;
    onDetach: (id: number) => void;
}

export function ProgrammeSubjectsTable({ attached, programmeId, onDetach }: Props) {
    if (!programmeId) return <div className="text-gray-500">Pasirinkite programą</div>;
    if (attached.length === 0) return <div className="text-gray-500">(nėra)</div>;

    return (
        <table className="min-w-full border border-gray-200 rounded text-sm">
            <thead className="bg-gray-50">
                <tr>
                    <th className="text-left px-3 py-2 border-b">Pavadinimas</th>
                    <th className="text-left px-3 py-2 border-b">Kreditai</th>
                    <th className="px-3 py-2 border-b w-32"></th>
                </tr>
            </thead>
            <tbody>
                {attached.map((s) => (
                    <tr key={s.id} className="even:bg-gray-50">
                        <td className="px-3 py-2">{s.title}</td>
                        <td className="px-3 py-2">{s.credits}</td>
                        <td className="px-3 py-2">
                            <button
                                className="text-red-600 hover:underline"
                                onClick={() => onDetach(s.id)}
                            >
                                Šalinti
                            </button>
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}
