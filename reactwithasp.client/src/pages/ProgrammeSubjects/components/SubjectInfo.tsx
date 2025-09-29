import { ISubject } from "../../../interfaces/ISubject"; 

type Props = { subject: ISubject };

export function SubjectInfo({ subject }: Props) {
    if (!subject) return null;
    return (
        <div>
            <div><b>Title:</b> {subject.title}</div>
            <div><b>Credits:</b> {subject.credits}</div>
        </div>
    );
}
