import { useEffect, useState } from 'react';
import { ILecturer } from "../../interfaces/ILecturer";
import { getApi, putApi, postApi, deleteApi } from "../../api";
import { Modal } from "../components/Modal";
import { LecturerForm } from "./components/LecturerForm";

export default function Lecturers() {
    const [lecturers, setLecturers] = useState<ILecturer[]>([])
    const [visibleModal, setVisibleModal] = useState<boolean>(false)
    const [editStudent, setEditStudent] = useState<ILecturer | undefined>()

    const getLecturers = () => getApi<ILecturer[]>('lecturers').then(s => s && setLecturers(s));

    const storeLecturer = (lecturer: ILecturer) => {
        setVisibleModal(false);
        if (lecturer.id) {
            putApi(`lecturers/${lecturer.id}`, lecturer)
                .then(r => getLecturers()).then(i => i);
            {/* updateLecturer */ }
        } else {
            postApi('lecturers', lecturer)
                .then(r => getLecturers()).then(i => i);
        }
    }

    const editHandler = (lecturer: ILecturer) => {
        setEditStudent(lecturer);
        setVisibleModal(true);
    }

    useEffect(() => {
        getLecturers().then(i => i)
    }, []);

    return <div>
        {
            visibleModal ? <Modal visibleModal={visibleModal} setVisibleModal={setVisibleModal} title='Dėstytojų forma' >
                <LecturerForm storeLecturer={storeLecturer} lecturer={editStudent} />
            </Modal> : null
        }
        <div className="text-3xl">Dėstytojai</div>
        {/* open modal */}
        <button type="button" onClick={() => { setEditStudent({ id: 0, firstName: '', lastName: '', email: '' }); setVisibleModal(true); }}
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded my-3"
        >Naujas dėstytojas</button>

        <div>{
            lecturers.map(lecturer => <div key={lecturer.id}><button type="button" onClick={() => editHandler(lecturer)}>{lecturer.firstName} {lecturer.lastName}</button>
                {lecturer.email}
                <button type="button" onClick={() => deleteApi(`lecturers/${lecturer.id}`, {}).then(() => getLecturers())} className="bg-red-500 hover:bg-red-700 text-white 
                        font-bold py-1 px-2 rounded my-2">Delete</button>
            </div>)
        }</div>
    </div>
}
