import {useEffect, useState} from "react";
import {CourseFromDropdownSessionAdminPageDto} from "../../../../../api/course/queries/get-course-from-dropdown-session-admin-page-query.tsx";
import {useQuery} from "@tanstack/react-query";
import {CourseService} from "../../../../../api/course/course-service.tsx";
import {Response} from "../../../../../api/utilities/model/Response.tsx";

export const useCoursesDictionary = () => {
    const [courses, setCourses] = useState<Record<string, CourseFromDropdownSessionAdminPageDto>>({});
    const {data, isLoading, isError} = useQuery<Response<CourseFromDropdownSessionAdminPageDto[]>, Error>({
        queryKey: ["courseFromDropdownSessionAdminPage"],
        queryFn: () => CourseService.GetFromDropdownSessionAdminPage({}),
    });

    useEffect(() => {
        if (!data?.data) return;
        const dict = data.data.reduce((acc, course) => {
            acc[course.id] = course;
            return acc;
        }, {} as Record<string, CourseFromDropdownSessionAdminPageDto>);
        setCourses(dict);
    }, [data]);

    return {courses, list: data?.data ?? [], isLoading, isError};
};