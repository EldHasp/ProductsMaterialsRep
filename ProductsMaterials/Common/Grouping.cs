using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>Реализация IGrouping</summary>
    /// <typeparam name="Tkey">Тип ключа</typeparam>
    /// <typeparam name="TElement">Тип элементов</typeparam>
    public class Grouping<Tkey, TElement> : IGrouping<Tkey, TElement>
    {
        public Tkey Key { get; }

        /// <summary>Лист элементов группы</summary>
        public List<TElement> Elements { get; } = new List<TElement>();

        public IEnumerator<TElement> GetEnumerator()
            => Elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>Конструктор задающий ключ</summary>
        /// <param name="key">Ключ</param>
        public Grouping(Tkey key)
            => Key = key;

        /// <summary>Конструктор задающий ключ и последовательность элементов</summary>
        /// <param name="key">Ключ</param>
        /// <param name="elements">Последовательность элементов</param>
        public Grouping(Tkey key, IEnumerable<TElement> elements)
            : this(key)
            => Elements.AddRange(elements);

        /// <summary>Конструктор задающий ключ и последовательность элементов</summary>
        /// <param name="key">Ключ</param>
        /// <param name="elements">Перечень элементов</param>
        public Grouping(Tkey key, params TElement[] elements)
            : this(key, (IEnumerable<TElement>)elements) { }

        /// <summary>Метод добавления элемента</summary>
        /// <param name="element">Элемент</param>
        /// <remarks>Реализован для инициализатора</remarks>
        public void Add(TElement element)
            => Elements.Add(element);
    }
}
