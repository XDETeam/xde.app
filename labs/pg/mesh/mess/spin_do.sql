create or replace procedure mesh.spin_do(
    _url text,
    _content xml = null
)
language plpgsql
as $$
declare
    __existing bigint;
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
        author = __author
    ;

    -- Close existing spin
    if __existing is not null then
        update
            mess.spin
        set
            content = _content
        where
            id = __existing
        ;
    end if;

    -- Open new spin
    insert into mess.spin(
        chain,
        author,
        url,
        visited
    ) values (
        __existing,
        __author,
        _url,
        __now
    );
end $$;
