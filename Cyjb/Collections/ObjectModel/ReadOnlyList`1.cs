using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Cyjb.Collections.ObjectModel
{

	/// <summary>
	/// 为泛型只读列表提供基类。
	/// </summary>
	/// <typeparam name="T">列表中的元素类型。</typeparam>
	public class ReadOnlyList<T> : ListBase<T>
	{
		/// <summary>
		/// 空的只读泛型列表。
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static IList<T> empty;
		/// <summary>
		/// 获取空的只读泛型列表。
		/// </summary>
		/// <value>空的只读泛型列表。</value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
		public static IList<T> Empty
		{
			get
			{
				if (empty == null)
				{
					Interlocked.CompareExchange(ref empty, new ReadOnlyList<T>(new T[0]), null);
				}
				return empty;
			}
		}
		/// <summary>
		/// 初始化 <see cref="ReadOnlyList&lt;T&gt;"/> 类的新实例。
		/// </summary>
		protected ReadOnlyList() { }
		/// <summary>
		/// 将 <see cref="ReadOnlyList&lt;T&gt;"/> 类的新实例初始化为指定列表的包装。
		/// </summary>
		/// <param name="list">由新的列表包装的集合。</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="list"/> 为 <c>null</c>。</exception>
		public ReadOnlyList(IList<T> list)
			: base(list)
		{
			ExceptionHelper.CheckArgumentNull(list, "list");
		}
		/// <summary>
		/// 将 <see cref="ReadOnlyList&lt;T&gt;"/> 类的新实例初始化为指定数组的包装。
		/// </summary>
		/// <param name="array">由新的列表包装的数组。</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="array"/> 为 <c>null</c>。</exception>
		public ReadOnlyList(T[] array)
			: base(new ArrayAdapter<T>(array))
		{ }
		/// <summary>
		/// 将 <see cref="ReadOnlyList&lt;T&gt;"/> 类的新实例初始化为指定迭代器中数据的包装。
		/// </summary>
		/// <param name="enumerable">由新的列表包装的迭代器中的数据。</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="enumerable"/> 为 <c>null</c>。</exception>
		public ReadOnlyList(IEnumerable<T> enumerable)
			: base(new ArrayAdapter<T>(enumerable.ToArray()))
		{ }

		#region ICollection<T> 成员

		/// <summary>
		/// 获取一个值，该值指示 <see cref="ReadOnlyList&lt;T&gt;"/> 是否为只读。
		/// </summary>
		/// <value>如果 <see cref="ReadOnlyList&lt;T&gt;"/> 为只读，则为 <c>true</c>；
		/// 否则为 <c>false</c>。</value>
		public override bool IsReadOnly
		{
			get { return true; }
		}

		#endregion // ICollection<T> 成员

	}
}