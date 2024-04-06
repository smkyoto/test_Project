(function() {
    const _t = init('com.crocro.util');

    // dateFormat | 日付時間フォーマット
    _t.dateFormat = function(txt, d) {
        if (d === undefined) {d = new Date()}
        const dgt = (m, n) => ('0000' + m).substr(-n);
        const arr = [
             {k: 'YYYY', v: d.getFullYear()}
            ,{k: 'YY',   v: dgt(d.getFullYear(), 2)}
            ,{k: 'MM',   v: dgt(d.getMonth() + 1, 2)}
            ,{k: 'M',    v: d.getMonth() + 1}
            ,{k: 'DD',   v: dgt(d.getDate(), 2)}
            ,{k: 'D',    v: d.getDate()}
            ,{k: 'hh',   v: dgt(d.getHours(), 2)}
            ,{k: 'h',    v: d.getHours()}
            ,{k: 'mm',   v: dgt(d.getMinutes(), 2)}
            ,{k: 'm',    v: d.getMinutes()}
            ,{k: 'ss',   v: dgt(d.getSeconds(), 2)}
            ,{k: 's',    v: d.getSeconds()}
            ,{k: 'iii',  v: dgt(d.getMilliseconds(), 3)}
            ,{k: 'i',    v: d.getMilliseconds()}
        ];
        arr.forEach(x => {txt = txt.replace(x.k, x.v)});
        return txt;
    };

    // 初期化用関数
    function init(p) {
        try {return module.exports = {}} catch(e) {}
        return ((o, p) => {
            p.split('.').forEach(k => o = o[k]||(o[k]={}));
            return o})(window, p);
    };
})();