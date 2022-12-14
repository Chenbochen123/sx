//点击插入按钮
var commandcolumn_insert = function (command, record) {
    if (command.toLowerCase() != "insert") {
        return;
    }
    var index = record.data.SeqIdx;
    grid = App.gridPanelCenter;
    store = grid.store;
    var r = Ext.create('gridPanelCenterStoreModel', {
        SeqIdx: index
    })

    for (i = 0; i < store.data.items.length ; i++) {
        if (i >= store.pageSize - 1) {
            store.data.removeAt(i);
            i--;
            continue;
        }
        if (store.data.items[i].data.SeqIdx > index) {
            store.data.items[i].data.SeqIdx++;
        }
    }
    record.data.SeqIdx++;
    store.data.add(r);
    grid.store.loadRecords(store.data.items, false);
}
//点击添加按钮
var commandcolumn_add = function (command, record) {
    if (command.toLowerCase() != "add") {
        return;
    }
    var index = record.data.SeqIdx;
    grid = App.gridPanelCenter;
    store = grid.store;
    var r = Ext.create('gridPanelCenterStoreModel', {
        SeqIdx: index + 1
    })

    for (i = 0; i < store.data.items.length ; i++) {
        if (i >= store.pageSize - 1) {
            store.data.removeAt(i);
            i--;
            continue;
        }
        if (store.data.items[i].data.SeqIdx > index) {
            store.data.items[i].data.SeqIdx++;
        }
    }
    store.data.add(r);
    grid.store.loadRecords(store.data.items, false);
}
//点击删除按钮
var commandcolumn_delete = function (command, record) {
    if (command.toLowerCase() != "delete") {
        return;
    }
    var index = record.data.SeqIdx;
    grid = App.gridPanelCenter;
    store = grid.store;

    for (i = 0; i < store.data.items.length ; i++) {
        if (i >= store.pageSize) {
            store.data.removeAt(i);
            i--;
            continue;
        }
        if (store.data.items[i].data.SeqIdx > index) {
            store.data.items[i].data.SeqIdx--;
        }
    }
    store.data.removeAt(record.index);
    var r = Ext.create('gridPanelCenterStoreModel', {
        SeqIdx: store.pageSize
    })
    store.data.add(r);
    grid.store.loadRecords(store.data.items, false);
}
//根据按钮类别进行删除和编辑操作
var commandcolumn_click = function (command, record) {
    commandcolumn_insert(command, record);
    commandcolumn_add(command, record);
    commandcolumn_delete(command, record);
    return false;
};




var showInfo = function (value, items) {
    if (!value) {
        return value;
    }
    for (var i = 0; i < items.length; i++) {
        if (items[i].data.field1.toString().trim() == value.toString().trim()) {
            return items[i].data.field2;
        }
    }
    return value;
};
var showTemCode = function (value) {
    var items = App.setTemCode.store.data.items;
    return showInfo(value, items);
};
var showActionCode = function (value) {
    var items = App.setActionCode.store.data.items;
    return showInfo(value, items);
};


