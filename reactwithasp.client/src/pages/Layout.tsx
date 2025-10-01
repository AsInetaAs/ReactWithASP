import { Link, Outlet, useFetchers, useNavigation } from "react-router-dom"; 

export function Layout() {
    const navigation = useNavigation();
    const fetchers = useFetchers();
    const fetcherInProgress = fetchers.some((f) =>
        ["loading", "submitting"].includes(f.state)
    );

    return <div className="flex flex-col min-h-screen">
    <header className="fixed top-0 left-0 w-full bg-amber-700 text-white p-4 z-50">
        <div className="text-3xl font-bold mb-2">GRADEBOOK</div>
        <nav>
            <ul className="flex gap-x-4">
                <li>
                    <Link to="/">Home</Link>
                </li>
                <li>
                    <Link to="/students">Students</Link>
                </li>
                    <li>
                        <Link to="/studyprogrammes">Study Programmes</Link>
                    </li>
                    <li>
                        <Link to="/lecturers">Lecturers</Link>
                    </li>
                    <li>
                        <Link to="/groups">Groups</Link>
                    </li>
                    <li>
                        <Link to="/programmesubjects">Programme Subjects</Link>
                    </li>
            </ul>
        </nav>
    </header>
        <main className="flex-grow pt-24 px-4">
            {navigation.state !== "idle" && <div className="m-1">Navigation in progress...</div>}
            {fetcherInProgress && <div className="m-1">Fetcher in progress...</div>}
       
        <Outlet />
        </main >
        <footer className='bg-gray-500 text-white text-sm flex content-center justify-center items-center h-10'>
            <div>Panevėžio kolegija</div>
        </footer>
    </div>
}
