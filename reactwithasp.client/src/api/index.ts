import { IStudent } from "../interfaces/IStudent";

function getFullURL(endpoint: string) {
    return `/api/${endpoint}`;
}

function getHeaders(header = {}) {
    return {
        headers: {
            "X-Requested-With": "XMLHttpRequest",
            ...header,
        },
    };
}

export async function getApi<T>(endpoint: string): Promise<T | undefined> {
    const fullUrl = getFullURL(endpoint);

    try {
        const result = await fetch(fullUrl, getHeaders());

        //  parodo klaidos teksta
        if (!result.ok) {
            const text = await result.text().catch(() => "");
            throw new Error(`HTTP ${result.status} ${result.statusText}\n${text}`);
        }

        // saugus JSON 
        let data: any = undefined;
        try {
            data = await result.json();
        } catch {
            data = undefined;
        }
        return data as T | undefined;
    } catch (e: any) {
        console.error(e?.message ?? e);
        return;
    }
}

export async function postApi(
    endpoint: string,
    data: Record<string, any>,
    methodType = ""
) {
    const fullUrl = getFullURL(endpoint);

    try {
        let _method = "POST";
        switch (methodType) {
            case "PUT":
                _method = "PUT";
                break;
            case "DELETE":
                _method = "DELETE";
                break;
            case "PATCH":
                _method = "PATCH";
                break;
            default:
                _method = "POST";
                break;
        }

        const postHeaders = { "Content-Type": "application/json" };
        const result = await fetch(fullUrl, {
            ...getHeaders(postHeaders),
            method: _method,
            body: JSON.stringify(data ?? {}),
        });

        //parodyti klaidos teksta
        if (!result.ok) {
            const text = await result.text().catch(() => "");
            throw new Error(`HTTP ${result.status} ${result.statusText}\n${text}`);
        }

        let response: any = null;
        try {
            response = await result.json();
        } catch {
            response = null; 
        }
        return response;
    }
    catch (e: any) {
        console.error(e?.message ?? e);
        throw e;
    }
}

export async function putApi(endpoint: string, data: Record<string, any>) {
    return postApi(endpoint, data, "PUT");
}

export async function deleteApi(endpoint: string, data: Record<string, any>) {
    return postApi(endpoint, data, "DELETE");
}

export async function createStudent(student: IStudent) {
    return await postApi("students", student);
}
