import { useEffect, useState } from 'react';
import { IStudyProgramme } from "@/interfaces/IStudyProgramme";
import { getApi, putApi, postApi, deleteApi } from "@/api";
import { Modal } from "@/pages/components/Modal";
import { StudyProgrammeForm } from "./components/StudyProgrammeForm";

export default function StudyProgramme() {
    const [studyProgrammes, setStudyProgrammes] = useState<IStudyProgramme[]>([])
    const [visibleModal, setVisibleModal] = useState<boolean>(false)
    const [editStudyProgramme, setEditStudyProgramme] = useState<IStudyProgramme | undefined>()

    const getStudyProgrammes = () => getApi<IStudyProgramme[]>('programmes').then(s => s && setStudyProgrammes(s));

    const storeStudyProgramme = (studyProgramme: IStudyProgramme) => {
        setVisibleModal(false);
        if (studyProgramme.id) {
            putApi(`programmes/${studyProgramme.id}`, studyProgramme)
                .then(r => getStudyProgrammes()).then(i => i);
            {/* updateStudyProgramme */ }
        } else {
            postApi('programmes', studyProgramme)
                .then(r => getStudyProgrammes()).then(i => i);
        }
    }

    const editHandler = (studyProgramme: IStudyProgramme) => {
        setEditStudyProgramme(studyProgramme);
        setVisibleModal(true);
    }

    useEffect(() => {
        getStudyProgrammes().then(i => i)
    }, []);

    return <div>
        {
            visibleModal ? <Modal visibleModal={visibleModal} setVisibleModal={setVisibleModal} title='Studijų programa' >
                <StudyProgrammeForm storeStudyProgramme={storeStudyProgramme} studyProgramme={editStudyProgramme} />
            </Modal> : null
        }
        <div className="text-3xl">Studijų programos</div>
        {/* open modal */}
        <button type="button" onClick={() => {
            setEditStudyProgramme({ id: 0, title: '', description: '', duration: 1 });
            setVisibleModal(true);
        }} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded my-3">
            Nauja studijų programa
        </button>

        <div>{
            studyProgrammes.map(studyProgramme => <div key={studyProgramme.id}><button type="button" onClick={() => editHandler(studyProgramme)}>{studyProgramme.title}</button>
                {studyProgramme.description}
                <button type="button" onClick={() => deleteApi(`programmes/${studyProgramme.id}`, {}).then(() => getStudyProgrammes())} className="bg-red-500 hover:bg-red-700 text-white 
                        font-bold py-1 px-2 rounded my-2">Delete</button>
            </div>)
        }</div>
    </div>
}
