create or replace procedure mesh.spin_do(
    _url text,
    _content xml = null,
    _now timestamp = current_timestamp
)
language plpgsql
as $$
declare
    __existing int;
    __spin int;
    __author int;
    __url int;
begin
    select
        id
    into
        __author
    from
         mess.profile
    where
        name = current_user
    ;

    if __author is null then
        raise 'Profile % not found', current_user using
            errcode = '0A000',
            hint = 'User should be registered in the mess.profile table'
        ;
    end if;

    select
        id
    into
        __existing
    from
        mess.spin
    where
        profile = __author
        and spin is null
    ;

    select
        id
    into
        __url
    from
        mess.url
    where
        url = _url::ltree
    ;

    if __url is null then
        insert into mess.url (url) values (_url::ltree) returning id into __url;
    end if;

    -- Open new spin
    insert into mess.spin(
        profile,
        url,
        visited
    ) values (
        __author,
        __url,
        _now
    ) returning
        id
    into
        __spin
    ;

    -- Close existing spin
    if __existing is not null then
        update
            mess.spin
        set
            content = _content,
            spin = __spin
        where
            id = __existing
        ;
    end if;
end $$;
