import { useForm } from "react-hook-form";
import { ILecturer } from "../../../interfaces/ILecturer";
import { useEffect } from "react";
import { formStyle } from "../../../styles/formStyle";
//import { createLecturer } from "../../../api";




type LecturerFormProps = { lecturer: ILecturer | undefined; storeLecturer: (data: ILecturer) => void }

export function LecturerForm(props: LecturerFormProps) {
    const { lecturer, storeLecturer } = props;
    const { register, handleSubmit, reset } = useForm<ILecturer>({ defaultValues: lecturer })

    useEffect(() => {
        reset(lecturer);
    }, [lecturer, reset]);

    return (
        <form onSubmit={handleSubmit(async (data) => {
            storeLecturer(data);

        })} className="flex flex-col gap-3">
            <input type="hidden" {...register("id")} />

            <div>
                <label htmlFor="firstName" className={formStyle.label}>Vardas</label>
                <input id="firstName" className={formStyle.input} {...register("firstName", { required: true, maxLength: 20 })} defaultValue={lecturer?.firstName || ''} />
            </div>

            <div>
                <label htmlFor="lastName" className={formStyle.label}>Pavardė</label>
                <input id="lastName" className={formStyle.input} {...register("lastName", { required: true, maxLength: 20 })} defaultValue={lecturer?.lastName || ''} />
            </div>

            <div>
                <label htmlFor="email" className={formStyle.label}>El. paštas</label>
                <input id="email" className={formStyle.input} type="email" {...register("email")} defaultValue={lecturer?.email || ''} />
            </div>
            <button className={formStyle.button} type="submit">
                {lecturer && lecturer.id !== 0 ? 'Atnaujinti' : 'Išsaugoti'}
            </button>
        </form>
    )
}
