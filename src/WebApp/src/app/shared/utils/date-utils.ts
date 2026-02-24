const timeUnits = ["s", "m", "H", "D", "Week", "Month", "Y", "Centuries"] as const;
const timeUnitValues = [1, 60, 3600, 86400, 604800, 2592000, 31536000, 3153600000];

/**
 * Formats a time value in seconds to a human-readable format with appropriate time units
 * @param value Time value in seconds to format
 * @param prefix Prefix of time
 * @returns Format of time to human readable format, for example: 1 H 30 m
 */
const formatTime = (value: number, prefix?: string) => {
    var cadena = 'Expected a positive integer';
    for (let iV = 0; iV < timeUnitValues.length - 1; iV++) {
        const nextUnit = timeUnitValues[iV + 1];
        if (value < nextUnit) {
            const newValue = Math.floor(value / timeUnitValues[iV]);
            const units = timeUnits[iV];
            cadena = `${newValue} ${units}`;
            const remainder = value % timeUnitValues[iV];
            if (remainder > 0) {
                cadena = formatTime(remainder, cadena);
            }
            break;
        }

    }

    return prefix ? `${prefix} ${cadena}` : cadena;

}

/**
 * Utility class for date and time related functions
 */
export class DateUtils {


    /**
     * Formats a time value in seconds to a human-readable format with appropriate time units
     * @param value Time value in seconds to format
     * @param prefix Prefix of time
     * @returns Format of time to human readable format, for example: 1 H 30 m
     */
    static formatTime(value: number, prefix?: string): string {
        return formatTime(value, prefix);
    }

}