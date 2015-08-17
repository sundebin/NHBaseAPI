using NHBaseThrift.Analyzing;
using NHBaseThrift.Attributes;
using NHBaseThrift.Enums;
using NHBaseThrift.Helpers;
using NHBaseThrift.Network;
using NHBaseThrift.Proxies;

namespace NHBaseThrift.TypeProcessors
{
    /// <summary>
    ///     bool����ThriftЭ���ֶδ��������ṩ����صĻ�������
    /// </summary>
    public class BoolThriftTypeProcessor : ThriftTypeProcessor
    {
        #region Constructor.

        /// <summary>
        ///     bool����ThriftЭ���ֶδ��������ṩ����صĻ�������
        /// </summary>
        public BoolThriftTypeProcessor()
        {
            _supportedType = typeof(bool);
            _expectedDataSize = 1;
        }

        #endregion

        #region Overrides of IntellectTypeProcessor

        /// <summary>
        ///     �ӵ������ͻ�����ת��ΪԪ����
        /// </summary>
        /// <param name="proxy">�ڴ�Ƭ�δ�����</param>
        /// <param name="attribute">�ֶ�����</param>
        /// <param name="analyseResult">�������</param>
        /// <param name="target">Ŀ�����ʵ��</param>
        /// <param name="isArrayElement">��ǰд���ֵ�Ƿ�Ϊ����Ԫ�ر�ʾ</param>
        public override void Process(IMemorySegmentProxy proxy, ThriftPropertyAttribute attribute, ToBytesAnalyseResult analyseResult, object target, bool isArrayElement = false, bool isNullable = false)
        {
            bool value = analyseResult.GetValue<bool>(target);
            proxy.WriteSByte((sbyte)attribute.PropertyType);
            proxy.WriteInt16(((short)attribute.Id).ToBigEndian());
            proxy.WriteBoolean(value);
        }

        /// <summary>
        ///     ��Ԫ����ת��Ϊ�������ͻ�����
        /// </summary>
        /// <param name="instance">Ŀ�����</param>
        /// <param name="result">�������</param>
        /// <param name="container">������������</param>
        public override GetObjectResultTypes Process(object instance, GetObjectAnalyseResult result, INetworkDataContainer container)
        {
            byte value;
            if(!container.TryReadByte(out value)) return GetObjectResultTypes.NotEnoughData;
            result.SetValue(instance, value == 0x01);
            return GetObjectResultTypes.Succeed;
        }

        #endregion
    }
}