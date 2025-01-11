import type {
    Errors,
    ValidationResponse,
    ServerValidationResponse
} from './types.ts';

const isEmpty = (value: string): boolean => !value || /^\s$/g.test(value);

const formatWord = (word: string): string => {
    word = word.trim();

    if (word.length === 1) {
        word = word.toUpperCase();
    }
    else {
        word = word.charAt(0).toUpperCase() + word.slice(1).toLowerCase();
    }

    return word;
}

interface Validations {
    firstName(name: string, errors: Errors): ValidationResponse<string>,
    lastName(name: string, errors: Errors): ValidationResponse<string>,
    name(name: string, errors: Errors): ValidationResponse<string>,
    email(email: string, errors: Errors): ValidationResponse<string>,
    phoneNumber(number: string, errors: Errors): ValidationResponse<string>,
    password(password: string, errors: Errors): ValidationResponse<string>,
    confirmPassword(password: string, confirmPassword: string, errors: Errors): ValidationResponse<string>,
    subject(subject: number, errors: Errors): ValidationResponse<number>,
    message(message: string, errors: Errors): ValidationResponse<string>
}

export const validations: Validations = {
    firstName(name: string, errors: Errors): ValidationResponse<string> {
        if (isEmpty(name)) {
            errors['FirstName'] = 'First Name is required';
        }
        else if (name.length < 2) {
            errors['FirstName'] = 'First Name must be at least 2 characters long';
        }
        else if (name.length > 50) {
            errors['FirstName'] = 'First Name can be no more than 50 characters long';
        }
        else {
            name = formatWord(name);
        }

        return {
            value: name,
            errors: errors
        };
    },
    lastName(name: string, errors: Errors): ValidationResponse<string> {
        if (isEmpty(name)) {
            errors['LastName'] = 'Last Name is required';
        }
        else if (name.length < 2) {
            errors['LastName'] = 'Last Name must be aqt least 2 characters long';
        }
        else if (name.length > 50) {
            errors['LastName'] = 'Last Name can be no more than 50 characters long';
        }
        else {
            name = formatWord(name);
        }

        return {
            value: name,
            errors: errors
        };
    },
    name(name: string, errors: Errors): ValidationResponse<string> {
        if (isEmpty(name)) {
            errors['Name'] = 'Name is required';
        }
        else if (name.length < 3) {
            errors['Name'] = 'Name must be at least 3 characters';
        }
        else if (name.length > 80) {
            errors['Name'] = 'Name can be no more than 80 characters long';
        }
        else {
            name = name.replace(/ +(?= )/g, '').trim();

            let split: string[] = name.split(' ');

            split = split.map((x: string): string => formatWord(x));

            name = split.join(' ');
        }

        return {
            value: name,
            errors: errors
        };
    },
    email(email: string, errors: Errors): ValidationResponse<string> {
        if (isEmpty(email)) {
            errors['Email'] = 'Email is required';
        }
        else if (email.length > 254) {
            errors['Email'] = 'Email can be no more than 254 characters long';
        }
        else if (!/(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])/g.test(email)) {
            errors['Email'] = 'Please enter a valid Email';
        }
        else {
            email = email.trim().toLowerCase();
        }

        return {
            value: email,
            errors: errors
        };
    },
    phoneNumber(number: string, errors: Errors): ValidationResponse<string> {
        if (isEmpty(number)) {
            errors['PhoneNumber'] = 'Phone Number is required';
        }
        else if (!/^(\(?[0-9]{3}\)?)\s?([0-9]{3})\-?([0-9]{4})$/g.test(number)) {
            errors['PhoneNumber'] = 'Phone Number must be valid'
        }
        else {
            number = number.replace(/\(\)\s\-/g, '');
        }

        return {
            value: number,
            errors: errors
        };
    },
    password(password: string, errors: Errors): ValidationResponse<string> {
        if (isEmpty(password)) {
            errors['Password'] = 'Password is required';
        }
        else if (password.length < 8) {
            errors['Password'] = 'Password must be at least 8 characters long';
        }
        else if (password.length > 128) {
            errors['Password'] = 'Password can be no more than 128 characters long';
        }
        else if (!/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/g.test(password)) {
            errors['Password'] = 'Password must have at least one upper case character, one lowercase character, one number and one special character';
        }
        else {
            password = password.trim();
        }

        return {
            value: password,
            errors: errors
        };
    },
    confirmPassword(password: string, confirmPassword: string, errors: Errors): ValidationResponse<string> {
        if (isEmpty(confirmPassword)) {
            errors['ConfirmPassword'] = 'Confirm Password is required';
        }
        else if (confirmPassword !== password) {
            errors['ConfirmPassword'] = 'Passwords do not match';
        }

        return {
            value: confirmPassword,
            errors: errors
        };
    },
    subject(subject: number, errors: Errors): ValidationResponse<number> {
        if (subject < 0) {
            errors['Subject'] = 'Subject is required';
        }

        return {
            value: subject,
            errors: errors
        };
    },
    message(message: string, errors: Errors): ValidationResponse<string> {
        if (isEmpty(message)) {
            errors['Message'] = 'Message is required';
        }
        else if (message.length < 5) {
            errors['Message'] = 'Message must be at least 5 characters';
        }
        else if (message.length > 300) {
            errors['Message'] = 'Message must be no more than 300 characters long';
        }
        else {
            const formatted: string = message.replace(/ +(?= )/g, '').trim();

            message = formatted.charAt(0).toUpperCase() + formatted.slice(1).toLowerCase();
        }

        return {
            value: message,
            errors: errors
        };
    }    
}

export const buildServerValidations = (serverResponse: ServerValidationResponse): Errors => {
    const errors: Errors = {};

    Object.keys(serverResponse.errors).forEach(key => {
        if (Array.isArray(serverResponse.errors[key])) {
            errors[key] = serverResponse.errors[key][0];
        }
    });

    return errors;
};
