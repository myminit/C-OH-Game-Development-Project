package com.coh.cohBackend.services;

import java.util.List;

public abstract class BaseService<T> {

    public abstract List<T> findByUserId(int userId);

    public abstract boolean updateStatus(int userId, int id, boolean status);
}
