import { useEffect, useState } from 'react';
import { IStudent } from "@/interfaces/IStudent";
import { getApi, putApi, postApi, deleteApi } from "@/api";
import {Modal} from "@/pages/components/Modal";
import {StudentForm} from "./components/StudentForm";

export default function Students() {
    const [students, setStudents] = useState<IStudent[]>([])
    const [visibleModal, setVisibleModal] = useState<boolean>(false)
    const [editStudent, setEditStudent] = useState<IStudent | undefined>()

const getStudents = () => getApi<IStudent[]>('students').then(s => s && setStudents(s));

const storeStudent = (student: IStudent) => {
  setVisibleModal(false);
  if (student.id) {
    putApi(`students/${student.id}`, student)
          .then(r => getStudents()).then(i => i);
      {/* updateStudent */ }
  } else {
      postApi('students', student)
          .then(r => getStudents()).then(i => i);
  }
}

const editHandler = (student: IStudent) => {
  setEditStudent(student);
  setVisibleModal(true);
}

    useEffect(() => {
        getStudents().then(i => i)
        }, []);
     
    return <div>
    {
        visibleModal ? <Modal visibleModal={visibleModal} setVisibleModal={setVisibleModal} title='Studentų forma' >
            <StudentForm storeStudent={storeStudent} student={editStudent} />
            </Modal> : null
            }
        <div className="text-3xl">Students</div>
        {/* open modal */}
        <button type="button" onClick={() => { setEditStudent({ id: 0, firstName: '', lastName: '', email: '' }); setVisibleModal(true); }}
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded my-3"
        >Naujas studentas</button>

            <div>{
                students.map(student => <div key={student.id}><button type="button" onClick={() => editHandler(student)}>{student.firstName} {student.lastName}</button>
                {student.email}
                    <button type="button" onClick={() => deleteApi(`students/${student.id}`, {}).then(() => getStudents())} className="bg-red-500 hover:bg-red-700 text-white 
                        font-bold py-1 px-2 rounded my-2">Delete</button>
                </div>)
       }</div>
    </div>
}
  