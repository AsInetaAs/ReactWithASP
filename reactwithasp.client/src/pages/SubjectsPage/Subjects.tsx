import { useEffect, useState } from 'react';
import { ISubject } from "@/interfaces/ISubject";
import { getApi, putApi, postApi, deleteApi } from "@/api";
import { Modal } from "@/pages/components/Modal";
import { SubjectForm } from "./components/SubjectForm";

export default function Subjects() {
    const [subjects, setSubjects] = useState<ISubject[]>([])
    const [visibleModal, setVisibleModal] = useState<boolean>(false)
    const [editSubject, setEditSubject] = useState<ISubject | undefined>()

    const getSubjects = () => getApi<ISubject[]>('subjects').then(s => s && setSubjects(s));

    const storeSubject = (subject: ISubject) => {
        setVisibleModal(false);
        if (subject.id) {
            putApi(`subjects/${subject.id}`, subject)
                .then(r => getSubjects()).then(i => i);
            {/* updateSubject */ }
        } else {
            postApi('subjects', subject)
                .then(r => getSubjects()).then(i => i);
        }
    }

    const editHandler = (subject: ISubject) => {
        setEditSubject(subject);
        setVisibleModal(true);
    }

    useEffect(() => {
        getSubjects().then(i => i)
    }, []);

    return <div>
        {
            visibleModal ? <Modal visibleModal={visibleModal} setVisibleModal={setVisibleModal} title='Dalyko forma' >
                <SubjectForm storeSubject={storeSubject} subject={editSubject} />
            </Modal> : null
        }
        <div className="text-3xl">Dalykai</div>
        {/* open modal */}
        <button type="button" onClick={() => { setEditSubject({ id: 0, title: '', credits: 1  }); setVisibleModal(true); }}
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded my-3"
        >Naujas dalykas</button>

        <div>{
            subjects.map(subject => <div key={subject.id}><button type="button" onClick={() => editHandler(subject)}>{subject.title}</button>
                {subject.credits}
                <button type="button" onClick={() => deleteApi(`subjects/${subject.id}`, {}).then(() => getSubjects())} className="bg-red-500 hover:bg-red-700 text-white 
                        font-bold py-1 px-2 rounded my-2">Delete</button>
            </div>)
        }</div>
    </div>
}
