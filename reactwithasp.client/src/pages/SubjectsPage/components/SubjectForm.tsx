import { useForm } from "react-hook-form";
import { ISubject } from "@/interfaces/ISubject";
import { useEffect } from "react";
import { formStyle } from "@/styles/formStyle";


type SubjectFormProps = { subject: ISubject | undefined; storeSubject: (data: ISubject) => void }

export function SubjectForm(props: SubjectFormProps) {
    const { subject, storeSubject } = props;
    const { register, handleSubmit, reset } = useForm<ISubject>({ defaultValues: subject })

    useEffect(() => {
        reset(subject);
    }, [subject, reset]);

    return (
        <form
            onSubmit={handleSubmit(async (data) => {storeSubject(data);})}
            className="flex flex-col gap-3"
        >
            <input type="hidden" {...register("id")} />

            <div>
                <label htmlFor="title" className={formStyle.label}> Pavadinimas </label>
                <input id="title" className={formStyle.input} {...register("title", { required: true, maxLength: 100 })} defaultValue={subject?.title || ""} />
            </div>

            <div>
                <label htmlFor="credits" className={formStyle.label}> Kreditai </label>
                <input id="credits" type="number" className={formStyle.input} {...register("credits", { required: true, min: 1, max: 30 })} defaultValue={subject?.credits || 0} />
            </div>

            <button className={formStyle.button} type="submit"> {subject && subject.id !== 0 ? "Atnaujinti" : "Išsaugoti"}
            </button>
        </form>
    );
}