/**
 * Authentication response model
 */
export interface AuthToken {
    token: string;
    expiresIn: number;
    refreshToken: string;
    refreshTokenExpiresIn: number;
    createdAt: string | Date;
}