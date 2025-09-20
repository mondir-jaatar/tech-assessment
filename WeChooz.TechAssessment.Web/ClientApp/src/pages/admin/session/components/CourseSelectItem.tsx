import {Avatar, Group, Text} from "@mantine/core";
import {IconBook} from "@tabler/icons-react";
import {CourseFromDropdownSessionAdminPageDto} from "../../../../../api/course/queries/get-course-from-dropdown-session-admin-page-query.tsx";

interface CourseSelectItemProps {
    course: CourseFromDropdownSessionAdminPageDto;
}

export const CourseSelectItem = ({course}: CourseSelectItemProps) => (
    <Group gap="sm">
        <Avatar radius="xl" size="sm" color="blue" variant="light">
            <IconBook size={16}/>
        </Avatar>
        <div>
            <Text>{course.name}</Text>
            <Text size="xs" c="dimmed">
                {course.trainer.firstName} {course.trainer.lastName}
            </Text>
        </div>
    </Group>
);
