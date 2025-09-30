import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom"; 
import Home from "./pages/HomePage/Home";
import { Layout } from "./pages/Layout";
import Students from "./pages/StudentsPage/Students";
import ProgrammeSubjects from "./pages/ProgrammeSubjects/ProgrammeSubjects";
import Lecturers from "./pages/LecturersPage/Lecturers";

export default function App() {

    const router = createBrowserRouter([
        {
            path: "/",
            Component: Layout,
            children: [
                {
                    index: true,
                    Component: Home
                },
                {
                    path: 'students', 
                    Component: Students
                },
                {
                    path: 'programmesubjects',
                    Component: ProgrammeSubjects
                },
                {
                    path: 'lecturers',
                    Component: Lecturers
                }

            ]
        },
    ]);
    return <RouterProvider router={router} />;
}