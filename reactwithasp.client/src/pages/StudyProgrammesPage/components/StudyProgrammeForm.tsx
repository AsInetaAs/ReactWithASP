import { useForm } from "react-hook-form";
import { IStudyProgramme } from "@/interfaces/IStudyProgramme";
import { useEffect } from "react";
import { formStyle } from "@/styles/formStyle";
//import { createStudyProgramme } from "../../../api";

type StudyProgrammeFormProps = { studyProgramme: IStudyProgramme | undefined; storeStudyProgramme: (data: IStudyProgramme) => void }

export function StudyProgrammeForm({ studyProgramme, storeStudyProgramme }: StudyProgrammeFormProps) {
    const { register, handleSubmit, reset } = useForm<IStudyProgramme>({ defaultValues: studyProgramme });

    useEffect(() => {
        reset(studyProgramme);
    }, [studyProgramme, reset]);

    return (
        <form onSubmit={handleSubmit(data => storeStudyProgramme(data))} className="flex flex-col gap-3">
            <input type="hidden" {...register("id")} />

            <div>
                <label htmlFor="title" className={formStyle.label}>Pavadinimas</label>
                <input id="title" className={formStyle.input} {...register("title", { required: true, maxLength: 100 })} />
            </div>

            <div>
                <label htmlFor="description" className={formStyle.label}>Aprašymas</label>
                <textarea id="description" className={formStyle.input} {...register("description", { required: true })} />
            </div>

            <div>
                <label htmlFor="duration" className={formStyle.label}>Trukmė</label>
                <input id="duration" className={formStyle.input} type="number" {...register("duration", { required: true, valueAsNumber: true, min: 1 })} />
            </div>

            <button className={formStyle.button} type="submit">
                {studyProgramme && studyProgramme.id !== 0 ? 'Atnaujinti' : 'Išsaugoti'}
            </button>
        </form>
    );
}
