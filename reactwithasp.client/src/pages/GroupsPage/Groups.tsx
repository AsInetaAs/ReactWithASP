import { useEffect, useState } from 'react';
import { IGroup } from "@/interfaces/IGroup";
import { getApi, putApi, postApi, deleteApi } from "@/api";
import { Modal } from "@/pages/components/Modal";
import { GroupForm } from "./components/GroupForm";

export default function Groups() {
    const [groups, setGroups] = useState<IGroup[]>([])
    const [visibleModal, setVisibleModal] = useState<boolean>(false)
    const [editGroup, setEditGroup] = useState<IGroup | undefined>()

    const getGroups = () => getApi<IGroup[]>('groups').then(s => s && setGroups(s));

    const storeGroup = (group: IGroup) => {
        setVisibleModal(false);
        if (group.id) {
            putApi(`groups/${group.id}`, group)
                .then(r => getGroups()).then(i => i);
            {/* updateGroup */ }
        } else {
            postApi('groups', group)
                .then(r => getGroups()).then(i => i);
        }
    }

    const editHandler = (group: IGroup) => {
        setEditGroup(group);
        setVisibleModal(true);
    }

    useEffect(() => {
        getGroups().then(i => i)
    }, []);

    return <div>
        {
            visibleModal ? <Modal visibleModal={visibleModal} setVisibleModal={setVisibleModal} title='Grupių forma' >
                <GroupForm storeGroup={storeGroup} group={editGroup} />
            </Modal> : null
        }
        <div className="text-3xl">Grupės</div>
        {/* open modal */}
        <button type="button" onClick={() => { setEditGroup({ id: 0, title: '', year: new Date().getFullYear() }); setVisibleModal(true); }}
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded my-3"
        >Nauja grupė</button>

        <div>{
            groups.map(group => <div key={group.id}><button type="button" onClick={() => editHandler(group)}>{group.title}</button>
                <button type="button" onClick={() => deleteApi(`groups/${group.id}`, {}).then(() => getGroups())} className="bg-red-500 hover:bg-red-700 text-white 
                        font-bold py-1 px-2 rounded my-2">Delete</button>
            </div>)
        }</div>
    </div>
}
