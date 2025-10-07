import { Layout } from "@/pages/Layout";
import Home from "@/pages/HomePage/Home";
import SignUp from "@/pages/auth/SignUpPage/SignUp";
import SignIn from "@/pages/auth/SignInPage/SignIn";
import {
    createBrowserRouter,
} from "react-router-dom";
import { ProtectedRoute } from "@/ProtectedRoute";
import Students from "@/pages/StudentsPage/Students";
import Lecturers from "@/pages/LecturersPage/Lecturers";
import Groups from "@/pages/GroupsPage/Groups";
import ProgrammeSubjects from "@/pages/ProgrammeSubjects/ProgrammeSubjects";
import StudyProgrammes from "@/pages/StudyProgrammesPage/StudyProgrammes";
import Dashboard from "@/pages/admin/DashboardPage/Dashboard";
import Subjects from "@/pages/SubjectsPage/Subjects";


export function router() {
    return createBrowserRouter([
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
                    element: <ProtectedRoute><Students /></ProtectedRoute >
                },
                {
                    path: 'lecturers',
                    element: <ProtectedRoute><Lecturers /></ProtectedRoute >
                },
                {
                    path: 'programmes',
                    element: <ProtectedRoute><StudyProgrammes /></ProtectedRoute >
                },
                {
                    path: 'programmesubjects',
                    element: <ProtectedRoute><ProgrammeSubjects /></ProtectedRoute >
                },
                {
                    path: 'groups',
                    element: <ProtectedRoute><Groups /></ProtectedRoute >
                },
                {
                    path: 'subjects',
                    element: <ProtectedRoute><Subjects /></ProtectedRoute >
                },
                {
                    path: 'auth/signup',

                    Component: SignUp
                },
                {
                    path: 'auth/signin',
                    Component: SignIn
                },
                {
                    path: 'admin/dashboard',
                    element: <ProtectedRoute><Dashboard /></ProtectedRoute >
                    }
            ]
        },
    ]);
}
