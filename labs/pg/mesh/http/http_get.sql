create function http_get(uri character varying) returns text
    language plpython3u
as
$$
    import requests

    return requests.get(uri).text
$$;

alter function http_get(varchar) owner to postgres;

