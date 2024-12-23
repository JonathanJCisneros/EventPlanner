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
    Birthday?: string,
    Subject?: string,
    Message?: string
}

export type ValidationResponse<T> = {
    value: T,
    errors: Errors
}
