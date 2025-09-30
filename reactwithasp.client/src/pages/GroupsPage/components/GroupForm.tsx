import { useForm } from "react-hook-form";
import { IGroup } from "../../../interfaces/IGroup";
import { useEffect } from "react";
import { formStyle } from "../../../styles/formStyle";

type GroupFormProps = { group: IGroup | undefined; storeGroup: (data: IGroup) => void }

export function GroupForm(props: GroupFormProps) {
    const { group, storeGroup } = props;
    const { register, handleSubmit, reset } = useForm<IGroup>({ defaultValues: group });

    useEffect(() => { reset(group); }, [group, reset]);

    return (
        <form onSubmit= { handleSubmit(data => storeGroup(data))
} className = "flex flex-col gap-3" >
    <input type="hidden" {...register("id") } />

        < div >
        <label htmlFor="title" className = { formStyle.label } > Pavadinimas </label>
            < input id = "title" className = { formStyle.input } {...register("title", { required: true, maxLength: 20 }) } />
                </div>

                < div >
                <label htmlFor="year" className = { formStyle.label } > Metai </label>
                    < input id = "year" type = "number" className = { formStyle.input } {...register("year", { required: true, min: 1900, max: 2100 }) } />
                        </div>

                        < button className = { formStyle.button } type = "submit" >
                            { group && group.id !== 0 ? "Atnaujinti" : "Išsaugoti"}
</button>
    </form>
  );
}
