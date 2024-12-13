interface Cookies {
    get(name: string): string,
    set(name: string, value: string, expirationDays: number): void,
    delete(name: string): void
}

const cookies: Cookies = {
    get: function (name: string): string {
        name += "=";

        const decodedCookie = decodeURIComponent(document.cookie);
        const cookies = decodedCookie.split(';');

        for (let i = 0; i < cookies.length; i++) {
            let c = cookies[i];

            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }

            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }

        return "";
    },
    set: function (name: string, value: string, expirationDays: number): void {
        const d = new Date();
        d.setTime(d.getTime() + (expirationDays * 24 * 60 * 60 * 1000));

        const expires = "expires=" + d.toUTCString();

        document.cookie = name + "=" + value + ";" + expires + ";path=/";
    },
    delete: function (name: string): void {
        this.set(name, '', -1);
    }
};

export default cookies;