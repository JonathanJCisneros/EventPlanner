export type FormResponse = {
    success: boolean,
    message: string
}

export type Errors = {
    FirstName?: string,
    LastName?: string,
    Name?: string,
    Email?: string,
    PhoneNumber?: string,
    Password?: string,
    ConfirmPassword?: string,
    Subject?: string,
    Message?: string
}

export type ValidationResponse<T> = {
    value: T,
    errors: Errors
}

export type ServerValidationResponse = {
    errors: {
        FirstName?: string[],
        LastName?: string[],
        Name?: string[],
        Email?: string[],
        PhoneNumber?: string[],
        Password?: string[],
        ConfirmPassword?: string[],
        Subject?: string[],
        Message?: string[]
    },
    status: number,
    title: string,
    traceId: string,
    type: string
}
