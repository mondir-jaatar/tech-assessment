import {PagedResponse} from './model/Response';
import { notifications } from '@mantine/notifications';

export function handleResponse(response: any) {
    if (response.data.succeeded != undefined) {
        return response.data;
    } else {
        //TODO show error with message as notification
    }
}

export function handleError(error: any) {
    if (error.response?.data?.IsConcurrencyError) {
        notifications.show({
            title: 'Erreur de concurrence',
            message: error.response?.data?.Message,
            color: 'red',
            autoClose: 5000,
        });
    }
    if (error.response.data.Succeeded != undefined) {
        return error.response.data;
    }

    throw error;
}

export function handlePagedResponse<T>(response: any) {
    return response.data as PagedResponse<T>;
}
