export interface UpdateParticipantsCommand
{
    sessionId: string;
    participants: UpdateParticipantDto[];
}

export interface UpdateParticipantDto
{
    id?: string;
    firstName: string;
    lastName: string;
    email: string;
    companyName: string;
}