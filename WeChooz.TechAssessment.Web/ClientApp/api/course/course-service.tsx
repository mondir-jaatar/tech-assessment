import {privateAxios} from "../utilities/axiosSetup.tsx";
import {handleError, handleResponse} from "../utilities/Response.tsx";
import {GetCourseFromDropdownSessionAdminPageQuery} from "./queries/get-course-from-dropdown-session-admin-page-query.tsx";

const BACK_END_URL = 'api/v1/Course';

export class CourseService {
    public static GetFromDropdownSessionAdminPage = (query: GetCourseFromDropdownSessionAdminPageQuery) => {
        return privateAxios
            .get(`${BACK_END_URL}/get-from-dropdown-session-admin-page`, { params: query })
            .then(handleResponse)
            .catch(handleError);
    };
}