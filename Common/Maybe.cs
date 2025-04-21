﻿using System;
using System.Collections.Generic;

namespace Common
{

    public struct Maybe<T>
    {
        private readonly T _value;
        public bool HasValue { get; }

        public Maybe(T value)
        {
            _value = value;
            HasValue = true;
        }

        public static Maybe<T> Some(T value) => new Maybe<T>(value);

        public static Maybe<T> None => new Maybe<T>();

        public Maybe<T> Where(Func<T, bool> predicate)
        {
            return HasValue && predicate(_value) ? this : None;
        }

        public IEnumerable<T> ToEnumerable()
        {
            if (HasValue) yield return _value;
        }

        public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
        {
            return HasValue ? some(_value) : none();
        }

        public T WithDefault(T defaultValue)
        {
            return HasValue ? _value : defaultValue;
        }

        public void Do(Action<T> action)
        {
            if (HasValue) action(_value);
        }

        public Maybe<T> Tap(Action<T> action)
        {
            if (HasValue)
                action(_value);
            return this;
        }

        public Maybe<T> TapNone(Action action)
        {
            if (!HasValue)
                action();
            return this;
        }

        public Maybe<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return HasValue ? Maybe<TResult>.Some(selector(_value)) : Maybe<TResult>.None;
        }

        // The magic, a.k.a bind, a.k.a flatMap
        public Maybe<TResult> SelectMany<TResult>(Func<T, Maybe<TResult>> selector)
        {
            return HasValue ? selector(_value) : Maybe<TResult>.None;
        }
    }

    public static class May
    {
        public static Maybe<T> Be<T>(T value)
        {
            return value == null ? Maybe<T>.None : Maybe<T>.Some(value);
        }
    }

    public static class Maybe
    {
        public static Maybe<TResult> MaybeSelect<T, TResult>(this T value, Func<T, TResult> selector)
        {
            return May.Be(value).Select(selector).SelectMany(May.Be);
        }
    }

    // public struct Unit
    // {
    //     public static readonly Unit Value = new Unit();
    // }


}