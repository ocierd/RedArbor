
export class ValidationUtils {


    private static readonly emailRegex: RegExp = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;



    /**
     * Validates if the provided string is a valid email address.
     * @param email The email address to validate.
     * @returns True if the email is valid, false otherwise.
     */
    static isValidEmail(email: string): boolean {
        return this.emailRegex.test(email);
    }


}
