CREATE FUNCTION mesh.http_get(uri text)
RETURNS text
LANGUAGE plpython3u
AS
$$
    import requests

    return requests.get(uri).text
$$;
