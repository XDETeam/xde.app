create or replace procedure mesh.spin_do(
    _url text,
    _content xml = null
)
language plpgsql
as $$
declare
    __existing bigint;
    __spin bigint;
    __author text = current_user;
    __now timestamp = current_timestamp;
begin
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

    raise notice 'Do for %', __existing;

    -- Open new spin
    insert into mess.spin(
        profile,
        url,
        visited
    ) values (
        __author,
        _url::ltree,
        __now
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
