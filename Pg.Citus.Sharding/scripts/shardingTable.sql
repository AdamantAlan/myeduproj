DO $$
BEGIN
  IF EXISTS (SELECT 1 FROM pg_proc WHERE proname='citus_is_coordinator')
     AND citus_is_coordinator() THEN
    PERFORM create_distributed_table('public."Orders"', 'CustomerId');
    -- дочерние таблицы: colocate_with => 'public."Orders"'
  END IF;
END$$;