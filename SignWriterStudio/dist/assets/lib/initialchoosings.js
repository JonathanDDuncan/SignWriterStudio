function getinitialchoosings() {

    var fsw = "M600x697S33000476x489S2ff00484x488S30a50495x490S30a40516x490S31430498x500S33e10501x520S33110510x511S36d10481x558S36a10498x538S37601546x560S37609463x560S14c10554x555S20500576x542S21600552x542S22800463x558S2f700562x519S22a00470x636S22e00583x587S23800487x631S24200506x631S24b00534x628S28800468x668S29900491x663S2e300534x665S2ed00568x587S2f100448x542S2f500564x655S36b10463x454S36c00493x447S38500534x443S37e0f440x559S29600511x663S38700476x695--Z01,1.48Z02,1.51Z05,1.21Z06,1.45Z08,1.63Z09,1.45Z10,1.6Z11,1.6Z25,0.79Z28,0.79Z29,0.79Z30,0.79";
    var signparser = peg.generate(fswpeg.sign);
    var parsed = signparser.parse(fsw);

    var sign = denormalizesign(parsed);

    var newsigns = [];
    sign.syms.forEach(function (symbol) {
        newsigns.push(createnewsign(symbol, 60, 60));
    });

    var choosings = [];
    newsigns.forEach(function (newsign) {
        choosings.push(getchoosingsign(newsign));
    });
    return choosings;
};

function denormalizesign(sign) {
    var newsign = {};
    newsign.width = 0;
    newsign.height = 0;
    newsign.text = "";
    newsign.x = 0;
    newsign.y = 0;
    newsign.laned = false;
    newsign.lane = "";
    newsign.left = 0;
    newsign.backfill = sign.sign.backgroundcolor ? sign.sign.backgroundcolor : "";

    var symbols = sign ? (sign.sign ? sign.sign.symbols : []) : []
    var symbolscolors = sign ? (sign.styling ? sign.styling.symbolscolors : []) : []
    var symbolszoom = sign ? (sign.styling ? sign.styling.symbolszoom : []) : []
    syms = denormalizesymbols(symbols, symbolscolors, symbolszoom);
    newsign.syms = syms;

    return newsign;
};

function denormalizesymbols(symbols, symbolscolors, symbolszoom) {
    var newsymbols = [];

    symbols.forEach(function (element) {
        var newsymbol = {};
        newsymbol.id = 0,
            newsymbol.selected = false,
            newsymbol.width = 0,
            newsymbol.height = 0,
            newsymbol.x = element.coord.x,
            newsymbol.y = element.coord.y,
            newsymbol.fontsize = 30,
            newsymbol.nwcolor = "white",
            newsymbol.pua = "",
            newsymbol.key = element.key,
            newsymbol.nbcolor = "black",
            newsymbol.size = 1

        newsymbols.push(newsymbol)
    }, this);

    if (symbolscolors) {
        symbolscolors.forEach(function (element) {
            var symbol = getsymbol(element.index, newsymbols)
            if (symbol) {
                symbol.nwcolor = element.back;
                symbol.nbcolor = element.fore;
            }
        }, this);
    }
    if (symbolszoom) {
        symbolszoom.forEach(function (element) {
            var symbol = getsymbol(element.index, newsymbols)
            if (symbol) {
                symbol.size = element.zoom;
                symbol.fontsize = symbol.fontsize * element.zoom;
                if (element.offset) {
                    symbol.x = symbol.x + element.offset.x - 500;
                    symbol.y = symbol.y + element.offset.y - 500;
                }
            }
        }, this);
    }

    return newsymbols;
};

function getsymbol(index, symbols) {
    return symbols[index - 1];
};



function createnewsign(symbol, x, y) {
    var sign = {};
    sign.syms = [];
    symbol.selected = false
    symbol.id = 0
    sign.syms.push(symbol);

    sign.backfill = "";
    sign.height = Math.trunc(symbol.height);
    sign.laned = false;
    sign.lane = ""

    sign.left = 0;
    sign.width = Math.trunc(symbol.width);
    sign.x = parseInt(symbol.x + x);
    sign.y = parseInt(symbol.y + y);

    sign.text = "";
    return sign;
}

function getchoosingsign(sign) {
    var choosing = {};
    choosing.displaySign = sign;
    choosing.valuestoAdd = sign.syms;
    choosing.value = getchoosingvalue(choosing.valuestoAdd);
    choosing.offset = { offsetx: sign.x - 500, offsety: sign.y - 500 };;

    return choosing;
}

function getchoosingvalue(values) {
    var key = 0;
    values.forEach(function (value) {
        key = value.key;
    });
    return key;
}
