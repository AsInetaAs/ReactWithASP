import { useEffect, useState } from "react";
import { getApi, postApi, deleteApi } from "../../api";
import { IStudyProgramme } from "../../interfaces/IStudyProgramme";
import { ISubject } from "../../interfaces/ISubject";
import { Modal } from "../components/Modal";
import { SubjectInfo } from "./components/SubjectInfo";
import { ProgrammeSubjectsTable } from "./components/ProgrammeSubjectsTable";

export default function ProgrammeSubjects() {
    const [programmes, setProgrammes] = useState<IStudyProgramme[]>([]);
    const [subjects, setSubjects] = useState<ISubject[]>([]);
    const [attached, setAttached] = useState<ISubject[]>([]);

    const [programmeId, setProgrammeId] = useState<number | undefined>();
    const [subjectId, setSubjectId] = useState<number | undefined>();
    const [visibleModal, setVisibleModal] = useState(false);

    const loadProgrammes = () => getApi<IStudyProgramme[]>("programmes").then(p => p && setProgrammes(p));
    const loadSubjects = () => getApi<ISubject[]>("subjects").then(s => s && setSubjects(s));
    const loadAttached = (pid?: number) => {
        if (typeof pid !== "number") return setAttached([]);
        getApi<ISubject[]>(`programmes/${pid}/subjects`).then(data =>
            setAttached(Array.isArray(data) ? data : [])
        );
    };

    useEffect(() => { loadProgrammes();loadSubjects();}, []);

    useEffect(() => {loadAttached(programmeId);}, [programmeId]);

    const handleAttach = async () => {
        if (!programmeId || !subjectId) return;
        await postApi(`programmes/${programmeId}/subjects/${subjectId}`, {});
        await loadAttached(programmeId);
    };

    const handleDetach = async (sid: number) => {
        if (!programmeId) return;
        await deleteApi(`programmes/${programmeId}/subjects/${sid}`, {});
        await loadAttached(programmeId);
    };

    const selectedSubject = subjects.find((s) => s.id === subjectId);

    return (
        <div className="pt-28 px-4 max-w-3xl mx-auto">
            <h1 className="text-3xl font-semibold mb-6">Programme subjects</h1>

            <div className="space-y-4">
                <div className="flex flex-col">
                    <label className="mb-1 font-medium">Studijų programa</label>
                    <select className="border rounded px-3 py-2" value={programmeId ?? ""} onChange={e => { const v = e.target.value ? parseInt(e.target.value) : undefined; setProgrammeId(v); setSubjectId(undefined); }}>
                        <option value="">-- select --</option>
                        {programmes.map(p => <option key={p.id} value={p.id}>{p.title}</option>)}
                    </select>
                </div>

                <div className="flex flex-col">
                    <label className="mb-1 font-medium">Studijų dalykas</label>
                    <select className="border rounded px-3 py-2" value={subjectId ?? ""} onChange={e => setSubjectId(e.target.value ? parseInt(e.target.value) : undefined)} disabled={!programmeId}>
                        <option value="">-- select --</option>
                        {subjects.map(s => <option key={s.id} value={s.id}>{s.title}</option>)}
                    </select>
                </div>

                <div className="flex gap-2">
                    <button type="button" className="bg-blue-600 text-white px-3 py-2 rounded disabled:opacity-50" disabled={!programmeId || !subjectId} onClick={handleAttach}>
                        Pridėti</button>
                    <button type="button" className="bg-gray-700 text-white px-3 py-2 rounded disabled:opacity-50" disabled={!selectedSubject} onClick={() => setVisibleModal(true)}>
                        Informacija</button>
                </div>
            </div>

            <section className="mt-8">
                <h2 className="text-xl font-medium mb-3">Priskirti dalykai</h2>
                <ProgrammeSubjectsTable attached={attached} programmeId={programmeId} onDetach={handleDetach} />
            </section>

            {visibleModal && selectedSubject && (
                <Modal visibleModal={visibleModal} setVisibleModal={setVisibleModal} title="Subject info"><SubjectInfo subject={selectedSubject} /></Modal>
            )}
        </div>
    );
}