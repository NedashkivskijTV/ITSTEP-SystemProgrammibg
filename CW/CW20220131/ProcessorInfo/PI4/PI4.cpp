//bool NComputerInfo::getCPUCount(QString& param_count)
//{
//    //备用(2014年11月3日--daodaoliang)
//    //systemInfo.dwActiveProcessorMask ---处理器掩码
//    //systemInfo.dwNumberOfProcessors ---处理器个数
//    //systemInfo.dwPageSize ---处理器分页大小
//    //systemInfo.dwProcessorType ---处理器类型
//    //systemInfo.lpMaximumApplicationAddress ---最大寻址单元
//    //systemInfo.lpMinimumApplicationAddress ---最小寻址单元
//    //systemInfo.wProcessorLevel ---处理器等级
//    //systemInfo.wProcessorRevision ---处理器版本
//    SYSTEM_INFO systemInfo;
//    GetSystemInfo(&systemInfo);
//    param_count = QString::number(systemInfo.dwNumberOfProcessors);
//    qDebug() << QString::number(systemInfo.wProcessorRevision, 16).toUpper();
//    return true;
//}