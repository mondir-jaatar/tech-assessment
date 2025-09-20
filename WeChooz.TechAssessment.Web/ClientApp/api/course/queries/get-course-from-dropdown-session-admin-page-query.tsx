export interface GetCourseFromDropdownSessionAdminPageQuery {
    
}

export interface CourseFromDropdownSessionAdminPageDto {
    id: string;
    name: string;
    trainer: TrainerFromDropdownSessionAdminPageDto;
}

export interface TrainerFromDropdownSessionAdminPageDto {
    firstName: string;
    lastName: string;
}