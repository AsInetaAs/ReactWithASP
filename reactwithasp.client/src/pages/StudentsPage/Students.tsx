import React, { useEffect, useState } from 'react';
import { IStudent } from "../../interfaces/IStudent";
import { getApi } from "../../api";

export default function Students() {
    const [students, setStudents] = useState<IStudent[]>([]);

    useEffect(() => {
        getApi<IStudent[]>('students').then(s => s && setStudents(s))
    }, []);

    return <div>
        <div>{
            students.map(student =>
                <div key={student.id}>{student.id} {student.fullName} {student.email}</div>)
        }</div>
    </div>
}