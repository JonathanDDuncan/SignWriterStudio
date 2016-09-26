    /*jshint bitwise: false*/
    /**
     * SignWriting 2010 JavaScript Library v1.6.1
     * https://github.com/Slevinski/sw10js
     * Copyright (c) 2007-2015, Stephen E Slevinski Jr
     * sw10.js is released under the MIT License.
     * modified and compiled with Closure Compiler by Jonathan Duncan
     */
    window["sw10"] = window["signwriting_2010"] = (function() {
        /**
         * @param {boolean=} style Some value (optional).
         */
        var key = function(text, style) {
            var keyvar = text.match(/S[123][0-9a-f]{2}[0-5][0-9a-f]([0-9]{3}x[0-9]{3})?/g);
            if (!keyvar) {
                return '';
            } else {
                return keyvar[0] + (style ? styling(text) : '');
            }
        };
        /**
         * @param {boolean=} style Some value (optional).
         */

        var fsw = function(text, style) {
            var fswvar = text.match(/(A(S[123][0-9a-f]{2}[0-5][0-9a-f])+)?[BLMR]([0-9]{3}x[0-9]{3})(S[123][0-9a-f]{2}[0-5][0-9a-f][0-9]{3}x[0-9]{3})*|S38[7-9ab][0-5][0-9a-f][0-9]{3}x[0-9]{3}/);
            if (!fswvar) {
                return '';
            } else {
                return fswvar[0] + (style ? styling(text) : '');
            }
        };
        var styling = function(text) {
            var sfsw = text.match(/-C?(P[0-9]{2})?(G\(([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+)\))?(D\(([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+)(,([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+))?\))?(Z[0-9]+(\.[0-9]+)?)?(\+(D[0-9]{2}\(([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+)(,([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+))?\))*(Z[0-9]{2},[0-9]+(\.[0-9]+)?(,[0-9]{3}x[0-9]{3})?)*)?/);
            if (!sfsw) {
                return '';
            } else {
                return sfsw[0];
            }
        };
        var mirror = function(keyvar) {
            keyvar = key(keyvar);
            if (!size(keyvar)) {
                return '';
            }
            var base = keyvar.slice(0, 4);
            var fillvar = keyvar.slice(4, 5);
            var rot = pInt(keyvar.slice(5, 6), 16);
            var key1 = base + "08";
            var key2 = base + "18";
            var rAdd = 0;
            if (size(key1) || size(key2)) {
                rAdd = 8;
            } else {
                if ((rot === 0) || (rot === 4)) {
                    rAdd = 0;
                }
                if ((rot === 1) || (rot === 5)) {
                    rAdd = 6;
                }
                if ((rot === 2) || (rot === 6)) {
                    rAdd = 4;
                }
                if ((rot === 3) || (rot === 7)) {
                    rAdd = 2;
                }
            }
            keyvar = '';
            while (!size(keyvar)) {
                rot += rAdd;
                if ((rot > 7) && (rAdd < 8)) {
                    rot = rot - 8;
                }
                if (rot > 15) {
                    rot = rot - 16;
                }
                keyvar = base + fillvar + rot.toString(16);
            }
            return keyvar;
        };
        var fill = function(keyvar, step) {
            keyvar = key(keyvar);
            if (!size(keyvar)) {
                return '';
            }
            if (step != -1) {
                step = 1;
            }
            var base = keyvar.slice(0, 4);
            var fillvar = pInt(keyvar.slice(4, 5));
            var rot = keyvar.slice(5, 6);
            keyvar = '';
            while (!size(keyvar)) {
                fillvar += step;
                if (fillvar > 5) {
                    fillvar = 0;
                }
                if (fillvar < 0) {
                    fillvar = 5;
                }
                keyvar = base + fillvar + rot;
            }
            return keyvar;
        };
        var rotate = function(keyvar, step) {
            keyvar = key(keyvar);
            if (!size(keyvar)) {
                return '';
            }
            if (step != -1) {
                step = 1;
            }
            var base = keyvar.slice(0, 4);
            var fillvar = keyvar.slice(4, 5);
            var rot = pInt(keyvar.slice(5, 6), 16);
            keyvar = '';
            while (!size(keyvar)) {
                if (rot > 7) {
                    rot += step;
                    if (rot > 15) {
                        rot = 8;
                    }
                    if (rot < 8) {
                        rot = 15;
                    }
                    keyvar = base + fillvar + rot.toString(16);
                } else {
                    rot -= step;
                    if (rot > 7) {
                        rot = 0;
                    }
                    if (rot < 0) {
                        rot = 7;
                    }
                    keyvar = base + fillvar + rot;
                }
            }
            return keyvar;
        };
        var scroll = function(keyvar, step) {
            keyvar = key(keyvar);
            if (!size(keyvar)) {
                return '';
            }
            if (step != -1) {
                step = 1;
            }
            var base = pInt(keyvar.slice(1, 4), 16) + step;
            var fillvar = keyvar.slice(4, 5);
            var rot = keyvar.slice(5, 6);
            var nkey = 'S' + base.toString(16) + fillvar + rot;
            if (size(nkey)) {
                return nkey;
            } else {
                return keyvar;
            }
        };
        var structure = function(division, keyvar, opt) {
            var arrs = {
                kind: ['S100', 'S37f', 'S387'],
                category: ['S100', 'S205', 'S2f7', 'S2ff', 'S36d', 'S37f', 'S387'],
                group: ['S100', 'S10e', 'S11e', 'S144', 'S14c', 'S186', 'S1a4', 'S1ba', 'S1cd', 'S1f5', 'S205', 'S216', 'S22a', 'S255', 'S265', 'S288', 'S2a6', 'S2b7', 'S2d5', 'S2e3', 'S2f7', 'S2ff', 'S30a', 'S32a', 'S33b', 'S359', 'S36d', 'S376', 'S37f', 'S387']
            };
            var arr = arrs[division];
            if (!arr) {
                return !keyvar ? [] : opt === "is" ? false : '';
            }
            if (!keyvar && !opt) {
                return arr;
            }
            if (!opt) {
                opt = '';
            }
            var adj;
            switch (opt) {
                case 'is':
                    return (arr.indexOf(keyvar.slice(0, 4)) === -1) ? false : true;
                case 'first':
                    return arr[0];
                case 'last':
                    return arr.slice(-1)[0];
                case 'prev':
                    adj = -2;
                    break;
                case '':
                    adj = -1;
                    break;
                case 'next':
                    adj = 0;
                    break;
                default:
                    return '';
            }
            var index = arr.length;
            var i;
            for (i = 0; i < arr.length; i++) {
                if (pInt(keyvar.slice(1, 4), 16) < pInt(arr[i].slice(1, 4), 16)) {
                    index = i;
                    break;
                }
            }
            index += adj;
            index = index < 0 ? 0 : index >= arr.length ? arr.length - 1 : index;
            return arr[index];
        };
        var type = function(typevar) {
            var start, end;
            switch (typevar) {
                case "writing":
                    start = '100';
                    end = '37e';
                    break;
                case "hand":
                    start = '100';
                    end = '204';
                    break;
                case "movement":
                    start = '205';
                    end = '2f6';
                    break;
                case "dynamic":
                    start = '2f7';
                    end = '2fe';
                    break;
                case "head":
                case "hcenter":
                    start = '2ff';
                    end = '36c';
                    break;
                case "vcenter":
                    start = '2ff';
                    end = '375';
                    break;
                case "trunk":
                    start = '36d';
                    end = '375';
                    break;
                case "limb":
                    start = '376';
                    end = '37e';
                    break;
                case "location":
                    start = '37f';
                    end = '386';
                    break;
                case "punctuation":
                    start = '387';
                    end = '38b';
                    break;
                default:
                    start = '100';
                    end = '38b';
                    break;
            }
            return [start, end];
        };
        /**
         * @param {string=} typevar Some value (optional).
         */
        var is = function(keyvar, typevar) {
            if (keyvar.length === 6 && !size(keyvar)) {
                return false;
            }
            var rangevar = type(typevar);
            var start = rangevar[0];
            var end = rangevar[1];
            var charvar = keyvar.slice(1, 4);
            return (pInt(start, 16) <= pInt(charvar, 16) && pInt(end, 16) >= pInt(charvar, 16));
        };
        /**
         * @param {string=} typevar Some value (optional).
         */
        var filter = function(fswvar, typevar) {
            var rangevar = type(typevar);
            var start = rangevar[0];
            var end = rangevar[1];
            var re = 'S' + range(start, end, "1") + '[0-5][0-9a-f][0-9]{3}x[0-9]{3}';
            var matches = fswvar.match(new RegExp(re, 'g'));
            if (matches) {
                return matches.join('');
            } else {
                return '';
            }
        };
        var random = function(typevar) {
            var rangevar = type(typevar);
            var start = rangevar[0];
            var end = rangevar[1];
            var rBase = Math.floor(Math.random() * (pInt(end, 16) - pInt(start, 16) + 1) + pInt(start, 16));
            var rFill = Math.floor(Math.random() * 6);
            var rRota = Math.floor(Math.random() * 16);
            var keyvar = "S" + rBase.toString(16) + rFill.toString(16) + rRota.toString(16);
            if (size(keyvar)) {
                return keyvar;
            } else {
                return random(typevar);
            }
        };
        var colorize = function(keyvar) {
            var color = '000000';
            if (is(keyvar, 'hand')) {
                color = '0000CC';
            }
            if (is(keyvar, 'movement')) {
                color = 'CC0000';
            }
            if (is(keyvar, 'dynamic')) {
                color = 'FF0099';
            }
            if (is(keyvar, 'head')) {
                color = '006600';
            } //    if (is(key,'trunk')) color = '000000';
            //    if (is(key,'limb')) color = '000000';
            if (is(keyvar, 'location')) {
                color = '884411';
            }
            if (is(keyvar, 'punctuation')) {
                color = 'FF9900';
            }
            return color;
        };
        var view = function(keyvar, fillone) {
            if (!is(keyvar)) {
                return '';
            }
            var prefix = keyvar.slice(0, 4);
            if (fillone) {
                return prefix + ((size(prefix + '00')) ? '0' : '1') + '0';
            } else {
                return prefix + ((is(prefix, 'hand') && !structure('group', prefix, 'is')) ? '1' : '0') + '0';
            }
        };
        /**
         * @param {Object=} hexval Some value (optional).
         */
        var code = function(text, hexval) {
            var keyvar;
            var fswvar = fsw(text);
            if (fswvar) {
                var pattern = 'S[123][0-9a-f]{2}[0-5][0-9a-f]';
                var matches = fswvar.match(new RegExp(pattern, 'g'));
                var i;
                for (i = 0; i < matches.length; i++) {
                    keyvar = matches[i];
                    fswvar = fswvar.replace(keyvar, code(keyvar, hexval));
                }
                return fswvar;
            }
            keyvar = key(text);
            if (!keyvar) {
                return '';
            }
            var codevar = 0x100000 + ((pInt(keyvar.slice(1, 4), 16) - 256) * 96) + ((pInt(keyvar.slice(4, 5), 16)) * 16) + pInt(keyvar.slice(5, 6), 16) + 1;
            return hexval ? codevar.toString(16).toUpperCase() : String.fromCharCode(0xD800 + ((codevar - 0x10000) >> 10), 0xDC00 + ((codevar - 0x10000) & 0x3FF));
        };
        /**
         * @param {Object=} hexval Some value (optional).
         */
        var uni8 = function(text, hexval) {
            var keyvar;
            var fswvar = fsw(text);
            if (fswvar) {
                var pattern = 'S[123][0-9a-f]{2}[0-5][0-9a-f]';
                var matches = fswvar.match(new RegExp(pattern, 'g'));
                var i;
                for (i = 0; i < matches.length; i++) {
                    keyvar = matches[i];
                    fswvar = fswvar.replace(keyvar, uni8(keyvar, hexval));
                }
                return fswvar;
            }
            keyvar = key(text);
            if (!keyvar) {
                return '';
            }
            var base = pInt(keyvar.substr(1, 3), 16) + pInt('1D700', 16);
            var uni8Var = hexval ? base.toString(16).toUpperCase() : String.fromCharCode(0xD800 + ((base - 0x10000) >> 10), 0xDC00 + ((base - 0x10000) & 0x3FF));
            var fillvar = keyvar.substr(4, 1);
            if (fillvar != "0") {
                fillvar = pInt(fillvar, 16) + pInt('1DA9A', 16);
                uni8Var += hexval ? fillvar.toString(16).toUpperCase() : String.fromCharCode(0xD800 + ((fillvar - 0x10000) >> 10), 0xDC00 + ((fillvar - 0x10000) & 0x3FF));
            }
            var rotation = keyvar.substr(5, 1);
            if (rotation != "0") {
                rotation = pInt(rotation, 16) + pInt('1DAA0', 16);
                uni8Var += hexval ? rotation.toString(16).toUpperCase() : String.fromCharCode(0xD800 + ((rotation - 0x10000) >> 10), 0xDC00 + ((rotation - 0x10000) & 0x3FF));
            }
            return uni8Var;
        };
        /**
         * @param {Object=} hexval Some value (optional).
         */
        var pua = function(text, hexval) {
            var fswvar = fsw(text);
            if (fswvar) {
                var str, codevar, coord, keyvar1, puavar;
                codevar = pInt('FD800', 16);
                fswvar = fswvar.replace('A', hexval ? (codevar).toString(16).toUpperCase() : String.fromCharCode(0xD800 + (((codevar) - 0x10000) >> 10), 0xDC00 + (((codevar) - 0x10000) & 0x3FF)));
                fswvar = fswvar.replace('B', hexval ? (codevar + 1).toString(16).toUpperCase() : String.fromCharCode(0xD800 + (((codevar + 1) - 0x10000) >> 10), 0xDC00 + (((codevar + 1) - 0x10000) & 0x3FF)));
                fswvar = fswvar.replace('L', hexval ? (codevar + 2).toString(16).toUpperCase() : String.fromCharCode(0xD800 + (((codevar + 2) - 0x10000) >> 10), 0xDC00 + (((codevar + 2) - 0x10000) & 0x3FF)));
                fswvar = fswvar.replace('M', hexval ? (codevar + 3).toString(16).toUpperCase() : String.fromCharCode(0xD800 + (((codevar + 3) - 0x10000) >> 10), 0xDC00 + (((codevar + 3) - 0x10000) & 0x3FF)));
                fswvar = fswvar.replace('R', hexval ? (codevar + 4).toString(16).toUpperCase() : String.fromCharCode(0xD800 + (((codevar + 4) - 0x10000) >> 10), 0xDC00 + (((codevar + 4) - 0x10000) & 0x3FF)));
                var pattern = '[0-9]{3}x[0-9]{3}';
                var matches = fswvar.match(new RegExp(pattern, 'g'));
                var i;
                for (i = 0; i < matches.length; i++) {
                    str = matches[i];
                    coord = str.split('x');
                    coord[0] = pInt(coord[0]) + pInt('FDD0C', 16);
                    coord[1] = pInt(coord[1]) + pInt('FDD0C', 16);
                    puavar = hexval ? coord[0].toString(16).toUpperCase() : String.fromCharCode(0xD800 + ((coord[0] - 0x10000) >> 10), 0xDC00 + ((coord[0] - 0x10000) & 0x3FF));
                    puavar += hexval ? coord[1].toString(16).toUpperCase() : String.fromCharCode(0xD800 + ((coord[1] - 0x10000) >> 10), 0xDC00 + ((coord[1] - 0x10000) & 0x3FF));
                    fswvar = fswvar.replace(str, puavar);
                }
                pattern = 'S[123][0-9a-f]{2}[0-5][0-9a-f]';
                matches = fswvar.match(new RegExp(pattern, 'g'));
                for (i = 0; i < matches.length; i++) {
                    keyvar1 = matches[i];
                    fswvar = fswvar.replace(keyvar1, pua(keyvar1, hexval));
                }
                return fswvar;
            }
            var keyvar2 = key(text);
            if (!keyvar2) {
                return '';
            }
            var base = pInt(keyvar2.substr(1, 3), 16) + pInt('FD730', 16);
            var puavar2 = hexval ? base.toString(16).toUpperCase() : String.fromCharCode(0xD800 + ((base - 0x10000) >> 10), 0xDC00 + ((base - 0x10000) & 0x3FF));
            var fillvar = keyvar2.substr(4, 1);
            fillvar = pInt(fillvar, 16) + pInt('FD810', 16);
            puavar2 += hexval ? fillvar.toString(16).toUpperCase() : String.fromCharCode(0xD800 + ((fillvar - 0x10000) >> 10), 0xDC00 + ((fillvar - 0x10000) & 0x3FF));
            var rotation = keyvar2.substr(5, 1);
            rotation = pInt(rotation, 16) + pInt('FD820', 16);
            puavar2 += hexval ? rotation.toString(16).toUpperCase() : String.fromCharCode(0xD800 + ((rotation - 0x10000) >> 10), 0xDC00 + ((rotation - 0x10000) & 0x3FF));
            return puavar2;
        };
        var bbox = function(fswvar) {
            var rcoord = /[0-9]{3}x[0-9]{3}/g;
            var x, y, x1 = Number.MAX_VALUE,
                x2 = Number.MIN_VALUE,
                y1 = Number.MAX_VALUE,
                y2 = Number.MIN_VALUE;
            var coords = fswvar.match(rcoord);
            if (coords) {
                var i;
                for (i = 0; i < coords.length; i++) {
                    x = pInt(coords[i].slice(0, 3));
                    y = pInt(coords[i].slice(4, 7));
                    if (i === 0) {
                        x1 = x;
                        x2 = x;
                        y1 = y;
                        y2 = y;
                    } else {
                        x1 = Math.min(x1, x);
                        x2 = Math.max(x2, x);
                        y1 = Math.min(y1, y);
                        y2 = Math.max(y2, y);
                    }
                }
                return '' + x1 + ' ' + x2 + ' ' + y1 + ' ' + y2;
            } else {
                return '';
            }
        };
        var displace = function(text, x, y) {
            var xpos, ypos;
            var re = '[0-9]{3}x[0-9]{3}';
            var matches = text.match(new RegExp(re, 'g'));
            if (matches) {
                var i;
                for (i = 0; i < matches.length; i++) {
                    xpos = pInt(matches[i].slice(0, 3)) + x;
                    ypos = pInt(matches[i].slice(4, 7)) + y;
                    text = text.replace(matches[i], xpos + "X" + ypos);
                }
                text = text.replace(/X/g, "x");
            }
            return text;
        };
        var sizes = {};
        var size = function(text) {
            var sizevar, fswvar = fsw(text);
            if (fswvar) {
                var bboxvar = bbox(fswvar);
                bboxvar = bboxvar.split(' ');
                var x1 = bboxvar[0];
                var x2 = bboxvar[1];
                var y1 = bboxvar[2];
                var y2 = bboxvar[3];
                sizevar = (x2 - x1) + 'x' + (y2 - y1);
                if (sizevar === '0x0') {
                    return '';
                }
                return sizevar;
            }
            var keyvar = key(text);
            if (!keyvar) {
                return '';
            }
            if (sizes[keyvar]) {
                return sizes[keyvar];
            }
            var imgData, i, zoom = 2;
            var bound = 76 * zoom;
            var canvaser;
            if (!canvaser) {
                canvaser = document.createElement("canvas");
                canvaser.width = bound;
                canvaser.height = bound;
            }
            var canvasvar = canvaser;
            var context = canvasvar.getContext("2d");
            context.clearRect(0, 0, bound, bound);
            context.font = (30 * zoom) + "px 'SignWriting 2010'";
            context.fillText(code(keyvar), 0, 0);
            imgData = context.getImageData(0, 0, bound, bound).data;
            var ww;
            wloop:
                for (ww = bound - 1; ww >= 0; ww--) {
                    for (var hw = 0; hw < bound; hw++) {
                        for (var sw = 0; sw < 4; sw++) {
                            i = ww * 4 + (hw * 4 * bound) + sw;
                            if (imgData[i]) {
                                break wloop;
                            }
                        }
                    }
                }
            var width = ww;
            var hh;
            hloop:
                for (hh = bound - 1; hh >= 0; hh--) {
                    for (var wh = 0; wh < width; wh++) {
                        for (var sh = 0; sh < 4; sh++) {
                            i = wh * 4 + (hh * 4 * bound) + sh;
                            if (imgData[i]) {
                                break hloop;
                            }
                        }
                    }
                }
            var height = hh + 1;
            width = '' + Math.ceil(width / zoom);
            height = '' + Math.ceil(height / zoom);
            // Rounding error in chrome.  Manual fixes.
            if ('S1710d S1711d S1712d S17311 S17321 S17733 S1773f S17743 S1774f S17753 S1775f S16d33 S1713d S1714d S17301 S17329 S1715d'.indexOf(keyvar) > -1) {
                width = '20';
            }
            if ('S24c15 S24c30'.indexOf(keyvar) > -1) {
                width = '22';
            }
            if ('S2903b'.indexOf(keyvar) > -1) {
                width = '23';
            }
            if ('S1d203 S1d233'.indexOf(keyvar) > -1) {
                width = '25';
            }
            if ('S24c15'.indexOf(keyvar) > -1) {
                width = '28';
            }
            if ('S2e629'.indexOf(keyvar) > -1) {
                width = '29';
            }
            if ('S16541 S23425'.indexOf(keyvar) > -1) {
                width = '30';
            }
            if ('S2d316'.indexOf(keyvar) > -1) {
                width = '40';
            }
            if ('S2541a'.indexOf(keyvar) > -1) {
                width = '50';
            }
            if ('S1732f S17731 S17741 S17751'.indexOf(keyvar) > -1) {
                height = '20';
            }
            if ('S1412c'.indexOf(keyvar) > -1) {
                height = '21';
            }
            if ('S2a903'.indexOf(keyvar) > -1) {
                height = '31';
            }
            if ('S2b002'.indexOf(keyvar) > -1) {
                height = '36';
            }
            sizevar = width + 'x' + height;
            // Error in chrome.  Manual fix.
            if (sizevar === '0x0') {
                var sizefix = 'S1000815x30 S1000921x30 S1000a30x15 S1000b30x21 S1000c15x30 S1000d21x30 ';
                var ipos = sizefix.indexOf(keyvar);
                if (ipos === -1) {
                    return '';
                } else {
                    var iend = sizefix.indexOf(' ', ipos);
                    sizevar = sizefix.slice(ipos + 6, iend);
                }
            }
            sizes[keyvar] = sizevar;
            return sizevar;
        };
        /**
         * @param {string=} typevar Some value (optional).
         */
        var max = function(fswvar, typevar) {
            var rangevar = type(typevar);
            var start = rangevar[0];
            var end = rangevar[1];
            var re = 'S' + range(start, end, "1") + '[0-5][0-9a-f][0-9]{3}x[0-9]{3}';
            var matches = fswvar.match(new RegExp(re, 'g'));
            if (matches) {
                var keyvar, x, y, sizevar, output = '';
                var i;
                for (i = 0; i < matches.length; i++) {
                    keyvar = matches[i].slice(0, 6);
                    x = pInt(matches[i].slice(6, 9));
                    y = pInt(matches[i].slice(10, 13));
                    sizevar = size(keyvar).split('x');
                    output += keyvar + x + "x" + y + (x + pInt(sizevar[0])) + 'x' + (y + pInt(sizevar[1]));
                }
                return output;
            } else {
                return '';
            }
        };
        var norm = function(fswvar) {
            var minx, maxx, miny, maxy;
            var hbox = bbox(max(fswvar, 'hcenter'));
            var vbox = bbox(max(fswvar, 'vcenter'));
            var box = bbox(max(fswvar));
            if (!box) return "";
            if (vbox) {
                minx = pInt(vbox.slice(0, 3));
                maxx = pInt(vbox.slice(4, 7));
            } else {
                minx = pInt(box.slice(0, 3));
                maxx = pInt(box.slice(4, 7));
            }
            if (hbox) {
                miny = pInt(hbox.slice(8, 11));
                maxy = pInt(hbox.slice(12, 15));
            } else {
                miny = pInt(box.slice(8, 11));
                maxy = pInt(box.slice(12, 15));
            }
            var xcenter = pInt((minx + maxx) / 2);
            var ycenter = pInt((miny + maxy) / 2);
            var xdiff = 500 - xcenter;
            var ydiff = 500 - ycenter;
            var start = fswvar.match(/(A(S[123][0-9a-f]{2}[0-5][0-9a-f])+)?[BLMR]/);
            if (!start) {
                start = 'M';
            } else {
                start = start[0];
            }

            fswvar = start + pInt(box.slice(4, 7)) + "x" + pInt(box.slice(12, 15)) + filter(fswvar);
            return displace(fswvar, xdiff, ydiff);
        };
        var svg = function(text, options) {
            var fswvar = fsw(text);
            var stylingvar = styling(text);
            var keysize;
            if (!fswvar) {
                var keyvar = key(text);
                keysize = size(keyvar);
                if (!keysize) {
                    return '';
                }
                if (keyvar.length === 6) {
                    fswvar = keyvar + "500x500";
                } else {
                    fswvar = keyvar;
                }
            }
            if (!options) {
                options = {};
            }
            if (options.size) {
                options.size = parseFloat(options.size) || 'x';
            } else {
                options.size = 1;
            }
            if (options.colorize) {
                options.colorize = true;
            } else {
                options.colorize = false;
            }
            if (options.pad) {
                options.pad = pInt(options.pad);
            } else {
                options.pad = 0;
            }
            if (!options.line) {
                options.line = "black";
            } else {
                options.line = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.line) ? "#" + options.line : options.line;
            }
            if (!options.fill) {
                options.fill = "white";
            } else {
                options.fill = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.fill) ? "#" + options.fill : options.fill;
            }
            if (!options.back) {
                options.back = "";
            } else {
                options.back = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.back) ? "#" + options.back : options.back;
            }
            options.E = [];
            options.F = [];

            options.view = options.view === "key" ? "key" : options.view === "uni8" ? "uni8" : options.view === "pua" ? "pua" : "code";
            options.copy = options.copy === "code" ? "code" : options.copy === "uni8" ? "uni8" : options.copy === "pua" ? "pua" : "key";


            if (stylingvar) {
                var rs;
                rs = stylingvar.match(/C/);
                options.colorize = rs ? true : false;

                rs = stylingvar.match(/P[0-9]{2}/);
                if (rs) {
                    options.pad = pInt(rs[0].substring(1, rs[0].length));
                }

                rs = stylingvar.match(/G\(([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+)\)/);
                if (rs) {
                    var back = rs[0].substring(2, rs[0].length - 1);
                    options.back = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(back) ? "#" + back : back;
                }

                var stylings = stylingvar.split('+');
                rs = stylings[0].match(/D\(([0-9a-f]{3}([0-9a-f]{3})?|[a-zA-Z]+)(,([0-9a-f]{3}([0-9a-f]{3})?|[a-zA-Z]+))?\)/);
                if (rs) {
                    var colors1 = rs[0].substring(2, rs[0].length - 1).split(',');
                    if (colors1[0]) {
                        options.line = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors1[0]) ? "#" + colors1[0] : colors1[0];
                    }
                    if (colors1[1]) {
                        options.fill = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors1[1]) ? "#" + colors1[1] : colors1[1];
                    }
                }

                rs = stylings[0].match(/Z[0-9]+(\.[0-9]+)?/);
                if (rs) {
                    options.size = rs[0].substring(1, rs[0].length);
                }

                if (!stylings[1]) {
                    stylings[1] = '';
                }
                rs = stylings[1].match(/D[0-9]{2}\(([0-9a-f]{3}([0-9a-f]{3})?|[a-wyzA-Z]+)(,([0-9a-f]{3}([0-9a-f]{3})?|[a-wyzA-Z]+))?\)/g);
                if (rs) {
                    var i1;
                    for (i1 = 0; i1 < rs.length; i1++) {
                        var pos1 = pInt(rs[i1].substring(1, 3));
                        var colors2 = rs[i1].substring(4, rs[i1].length - 1).split(',');
                        if (colors2[0]) {
                            colors2[0] = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors2[0]) ? "#" + colors2[0] : colors2[0];
                        }
                        if (colors2[1]) {
                            colors2[1] = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors2[1]) ? "#" + colors2[1] : colors2[1];
                        }
                        options.E[pos1] = colors2;
                    }
                }

                rs = stylings[1].match(/Z[0-9]{2},[0-9]+(\.[0-9]+)?(,[0-9]{3}x[0-9]{3})?/g);
                if (rs) {
                    var i2;
                    for (i2 = 0; i2 < rs.length; i2++) {
                        var pos2 = pInt(rs[i2].substring(1, 3));
                        var sizevar1 = rs[i2].substring(4, rs[i2].length).split(',');
                        sizevar1[0] = parseFloat(sizevar1[0]);
                        options.F[pos2] = sizevar1;
                    }
                }
            }

            var r, rsym, rcoord, sym, syms, gelem, o;
            r = /(A(S[123][0-9a-f]{2}[0-5][0-9a-f])+)?[BLMR]([0-9]{3}x[0-9]{3})(S[123][0-9a-f]{2}[0-5][0-9a-f][0-9]{3}x[0-9]{3})*|S38[7-9ab][0-5][0-9a-f][0-9]{3}x[0-9]{3}/g;
            rsym = /S[123][0-9a-f]{2}[0-5][0-9a-f][0-9]{3}x[0-9]{3}/g;
            rcoord = /[0-9]{3}x[0-9]{3}/g;
            o = {};
            o.L = -1;
            o.R = 1;
            var x,
                x1 = 500,
                x2 = 500,
                y,
                y1 = 500,
                y2 = 500,
                k,
                w,
                h,
                l;
            k = fswvar.charAt(0);
            var bboxvar = bbox(fswvar);
            bboxvar = bboxvar.split(' ');
            x1 = pInt(bboxvar[0]);
            x2 = pInt(bboxvar[1]);
            y1 = pInt(bboxvar[2]);
            y2 = pInt(bboxvar[3]);
            if (k === 'S') {
                if (x1 === 500 && y1 === 500) {
                    var sizevar2 = keysize.split('x');
                    x2 = 500 + pInt(sizevar2[0]);
                    y2 = 500 + pInt(sizevar2[1]);
                } else {
                    x2 = 1000 - x1;
                    y2 = 1000 - y1;
                }
            }
            syms = fswvar.match(rsym);
            var i3;
            for (i3 = 0; i3 < syms.length; i3++) {
                sym = syms[i3].slice(0, 6);
                x = syms[i3].slice(6, 9);
                y = syms[i3].slice(10, 13);
                if (options.F[i3 + 1]) {
                    if (options.F[i3 + 1][1]) {
                        x = pInt(x) + pInt(options.F[i3 + 1][1].slice(0, 3)) - 500;
                        y = pInt(y) + pInt(options.F[i3 + 1][1].slice(4, 7)) - 500;
                        x1 = Math.min(x1, x);
                        y1 = Math.min(y1, y);
                    }
                    var keysized = size(sym);
                    if (keysized) {
                        keysized = keysized.split('x');
                        x2 = Math.max(x2, pInt(x) + (options.F[i3 + 1][0] * pInt(keysized[0])));
                        y2 = Math.max(y2, pInt(y) + (options.F[i3 + 1][0] * pInt(keysized[1])));
                    }

                }
                gelem = '<g transform="translate(' + x + ',' + y + ')">';
                gelem += '<text ';
                gelem += 'class="sym-fill" ';
                if (!options.css) {
                    gelem += 'style="pointer-events:none;font-family:\'SignWriting 2010 Filling\';font-size:' + (options.F[i3 + 1] ? 30 * options.F[i3 + 1][0] : 30) + 'px;fill:' + (options.E[i3 + 1] ? options.E[i3 + 1][1] ? options.E[i3 + 1][1] : options.fill : options.fill) + ';';
                    gelem += options.view === 'code' ? '' : '-webkit-font-feature-settings:\'liga\';font-feature-settings:\'liga\';';
                    gelem += '"';
                    //-moz-font-feature-settings:'liga';
                }
                gelem += '>';
                gelem += options.view === "key" ? sym : options.view === "uni8" ? uni8(sym) : options.view === "pua" ? pua(sym) : code(sym);
                gelem += '</text>';
                gelem += '<text ';
                gelem += 'class="sym-line" ';
                if (!options.css) {
                    gelem += 'style="';
                    gelem += options.view === options.copy ? '' : 'pointer-events:none;';
                    gelem += 'font-family:\'SignWriting 2010\';font-size:' + (options.F[i3 + 1] ? 30 * options.F[i3 + 1][0] : 30) + 'px;fill:' + (options.E[i3 + 1] ? options.E[i3 + 1][0] : options.colorize ? '#' + colorize(sym) : options.line) + ';';
                    gelem += options.view === 'code' ? '' : '-webkit-font-feature-settings:\'liga\';font-feature-settings:\'liga\';';
                    gelem += '"';
                }
                gelem += '>';
                gelem += options.view === "key" ? sym : options.view === "uni8" ? uni8(sym) : options.view === "pua" ? pua(sym) : code(sym);
                gelem += '</text>';
                gelem += '</g>';
                syms[i3] = gelem;
            }

            x1 = x1 - options.pad;
            x2 = x2 + options.pad;
            y1 = y1 - options.pad;
            y2 = y2 + options.pad;
            w = x2 - x1;
            h = y2 - y1;
            l = o[k] || 0;
            l = l * 75 + x1 - 400;
            var svgvar = '<svg xmlns="http://www.w3.org/2000/svg" ';
            if (options.classname) {
                svgvar += 'class="' + options.classname + '" ';
            }
            if (options.size != 'x') {
                svgvar += 'width="' + (w * options.size) + '" height="' + (h * options.size) + '" ';
            }
            svgvar += 'viewBox="' + x1 + ' ' + y1 + ' ' + w + ' ' + h + '">';
            if (options.view != options.copy) {
                svgvar += '<text style="font-size:0%;">';
                svgvar += options.copy === "code" ? code(text) : options.copy === "uni8" ? uni8(text) : options.copy === "pua" ? pua(text) : text;
                svgvar += '</text>';
            }
            if (options.back) {
                svgvar += '  <rect x="' + x1 + '" y="' + y1 + '" width="' + w + '" height="' + h + '" style="fill:' + options.back + ';" />';
            }
            svgvar += syms.join('') + "</svg>";
            if (options.laned) {
                svgvar = '<div style="padding:10px;position:relative;width:' + w + 'px;height:' + h + 'px;left:' + l + 'px;">' + svgvar + '</div>';
            }
            return svgvar;
        };
        var symbolsList = function (text, options) {
            try {
                
           
            var fswvar = fsw(text);
            var stylingvar = styling(text);
            var keysize;
            if (!fswvar) {
                var keyvar = key(text);
                keysize = size(keyvar);
                if (!keysize) {
                    return '';
                }
                if (keyvar.length === 6) {
                    fswvar = keyvar + "500x500";
                } else {
                    fswvar = keyvar;
                }
            }
            if (!options) {
                options = {};
            }
            if (options.size) {
                options.size = parseFloat(options.size) || 'x';
            } else {
                options.size = 1;
            }
            if (options.colorize) {
                options.colorize = true;
            } else {
                options.colorize = false;
            }
            if (options.pad) {
                options.pad = pInt(options.pad);
            } else {
                options.pad = 0;
            }
            if (!options.line) {
                options.line = "black";
            } else {
                options.line = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.line) ? "#" + options.line : options.line;
            }
            if (!options.fill) {
                options.fill = "white";
            } else {
                options.fill = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.fill) ? "#" + options.fill : options.fill;
            }
            if (!options.back) {
                options.back = "";
            } else {
                options.back = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.back) ? "#" + options.back : options.back;
            }
            options.E = [];
            options.F = [];

            options.view = options.view === "key" ? "key" : options.view === "uni8" ? "uni8" : options.view === "pua" ? "pua" : "code";
            options.copy = options.copy === "code" ? "code" : options.copy === "uni8" ? "uni8" : options.copy === "pua" ? "pua" : "key";


            if (stylingvar) {
                var rs;
                rs = stylingvar.match(/C/);
                options.colorize = rs ? true : false;

                rs = stylingvar.match(/P[0-9]{2}/);
                if (rs) {
                    options.pad = pInt(rs[0].substring(1, rs[0].length));
                }

                rs = stylingvar.match(/G\(([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+)\)/);
                if (rs) {
                    var back = rs[0].substring(2, rs[0].length - 1);
                    options.back = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(back) ? "#" + back : back;
                }

                var stylings = stylingvar.split('+');
                rs = stylings[0].match(/D\(([0-9a-f]{3}([0-9a-f]{3})?|[a-zA-Z]+)(,([0-9a-f]{3}([0-9a-f]{3})?|[a-zA-Z]+))?\)/);
                if (rs) {
                    var colors1 = rs[0].substring(2, rs[0].length - 1).split(',');
                    if (colors1[0]) {
                        options.line = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors1[0]) ? "#" + colors1[0] : colors1[0];
                    }
                    if (colors1[1]) {
                        options.fill = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors1[1]) ? "#" + colors1[1] : colors1[1];
                    }
                }

                rs = stylings[0].match(/Z[0-9]+(\.[0-9]+)?/);
                if (rs) {
                    options.size = rs[0].substring(1, rs[0].length);
                }

                if (!stylings[1]) {
                    stylings[1] = '';
                }
                rs = stylings[1].match(/D[0-9]{2}\(([0-9a-f]{3}([0-9a-f]{3})?|[a-wyzA-Z]+)(,([0-9a-f]{3}([0-9a-f]{3})?|[a-wyzA-Z]+))?\)/g);
                if (rs) {
                    var i1;
                    for (i1 = 0; i1 < rs.length; i1++) {
                        var pos1 = pInt(rs[i1].substring(1, 3));
                        var colors2 = rs[i1].substring(4, rs[i1].length - 1).split(',');
                        if (colors2[0]) {
                            colors2[0] = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors2[0]) ? "#" + colors2[0] : colors2[0];
                        }
                        if (colors2[1]) {
                            colors2[1] = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors2[1]) ? "#" + colors2[1] : colors2[1];
                        }
                        options.E[pos1] = colors2;
                    }
                }

                rs = stylings[1].match(/Z[0-9]{2},[0-9]+(\.[0-9]+)?(,[0-9]{3}x[0-9]{3})?/g);
                if (rs) {
                    var i2;
                    for (i2 = 0; i2 < rs.length; i2++) {
                        var pos2 = pInt(rs[i2].substring(1, 3));
                        var sizevar1 = rs[i2].substring(4, rs[i2].length).split(',');
                        sizevar1[0] = parseFloat(sizevar1[0]);
                        options.F[pos2] = sizevar1;
                    }
                }
            }

            var r, rsym, rcoord, sym, syms, gelem, o, exsyms, exsym;
            r = /(A(S[123][0-9a-f]{2}[0-5][0-9a-f])+)?[BLMR]([0-9]{3}x[0-9]{3})(S[123][0-9a-f]{2}[0-5][0-9a-f][0-9]{3}x[0-9]{3})*|S38[7-9ab][0-5][0-9a-f][0-9]{3}x[0-9]{3}/g;
            rsym = /S[123][0-9a-f]{2}[0-5][0-9a-f][0-9]{3}x[0-9]{3}/g;
            rcoord = /[0-9]{3}x[0-9]{3}/g;
            o = {};
            o.L = -1;
            o.R = 1;
            var x,
                x1 = 500,
                x2 = 500,
                y,
                y1 = 500,
                y2 = 500,
                k,
                w,
                h,
                l;
            k = fswvar.charAt(0);
            var bboxvar = bbox(fswvar);
            bboxvar = bboxvar.split(' ');
            x1 = pInt(bboxvar[0]);
            x2 = pInt(bboxvar[1]);
            y1 = pInt(bboxvar[2]);
            y2 = pInt(bboxvar[3]);
            if (k === 'S') {
                if (x1 === 500 && y1 === 500) {
                    var sizevar2 = keysize.split('x');
                    x2 = 500 + pInt(sizevar2[0]);
                    y2 = 500 + pInt(sizevar2[1]);
                } else {
                    x2 = 1000 - x1;
                    y2 = 1000 - y1;
                }
            }
            syms = fswvar.match(rsym);
            exsyms = [];
            var i3;
            for (i3 = 0; i3 < syms.length; i3++) {
                sym = syms[i3].slice(0, 6);
                x = syms[i3].slice(6, 9);
                y = syms[i3].slice(10, 13);
                if (options.F[i3 + 1]) {
                    if (options.F[i3 + 1][1]) {
                        x = pInt(x) + pInt(options.F[i3 + 1][1].slice(0, 3)) - 500;
                        y = pInt(y) + pInt(options.F[i3 + 1][1].slice(4, 7)) - 500;
                        x1 = Math.min(x1, x);
                        y1 = Math.min(y1, y);
                    }
                    var keysized = size(sym);
                    if (keysized) {
                        keysized = keysized.split('x');
                        exsym.width = parseInt(keysized[0]);
                        exsym.height = parseInt(keysized[1]);
                        x2 = Math.max(x2, pInt(x) + (options.F[i3 + 1][0] * pInt(keysized[0])));
                        y2 = Math.max(y2, pInt(y) + (options.F[i3 + 1][0] * pInt(keysized[1])));
                    }

                }
                exsym = new Object;
                exsym.x = parseInt(x);
                exsym.y = parseInt(y);
                var keysized1 = size(sym);
               
                keysized1 = keysized1.split('x');
                exsym.width = parseInt(keysized1[0]);
                exsym.height = parseInt(keysized1[1]);

                gelem = '<g transform="translate(' + x + ',' + y + ')">';
                gelem += '<text ';
                gelem += 'class="sym-fill" ';
                if (!options.css) {
                    exsym.fontsize = (options.F[i3 + 1] ? 30 * options.F[i3 + 1][0] : 30);
                    exsym.nwcolor = (options.E[i3 + 1] ? options.E[i3 + 1][1] ? options.E[i3 + 1][1] : options.fill : options.fill);
                    gelem += 'style="pointer-events:none;font-family:\'SignWriting 2010 Filling\';font-size:' + (options.F[i3 + 1] ? 30 * options.F[i3 + 1][0] : 30) + 'px;fill:' + (options.E[i3 + 1] ? options.E[i3 + 1][1] ? options.E[i3 + 1][1] : options.fill : options.fill) + ';';
                    gelem += options.view === 'code' ? '' : '-webkit-font-feature-settings:\'liga\';font-feature-settings:\'liga\';';
                    gelem += '"';
                    //-moz-font-feature-settings:'liga';
                }
                gelem += '>';
                exsym.pua = pua(sym);
                exsym.code = code(sym).codePointAt();
                exsym.key = sym;
                gelem += options.view === "key" ? sym : options.view === "uni8" ? uni8(sym) : options.view === "pua" ? pua(sym) : code(sym);
                gelem += '</text>';
                gelem += '<text ';
                gelem += 'class="sym-line" ';
                if (!options.css) {
                    gelem += 'style="';
                    gelem += options.view === options.copy ? '' : 'pointer-events:none;';
                    exsym.nbcolor = (options.E[i3 + 1] ? options.E[i3 + 1][0] : options.colorize ? '#' + colorize(sym) : options.line);
                    gelem += 'font-family:\'SignWriting 2010\';font-size:' + (options.F[i3 + 1] ? 30 * options.F[i3 + 1][0] : 30) + 'px;fill:' + (options.E[i3 + 1] ? options.E[i3 + 1][0] : options.colorize ? '#' + colorize(sym) : options.line) + ';';
                    gelem += options.view === 'code' ? '' : '-webkit-font-feature-settings:\'liga\';font-feature-settings:\'liga\';';
                    gelem += '"';
                }
                gelem += '>';
                gelem += options.view === "key" ? sym : options.view === "uni8" ? uni8(sym) : options.view === "pua" ? pua(sym) : code(sym);
                gelem += '</text>';
                gelem += '</g>';
                syms[i3] = gelem;
                exsyms[i3] = exsym;

            }

            x1 = x1 - options.pad;
            x2 = x2 + options.pad;
            y1 = y1 - options.pad;
            y2 = y2 + options.pad;
            w = x2 - x1;
            h = y2 - y1;
            l = o[k] || 0;
            l = l * 75 + x1 - 400;
            var exsign = new Object;
        
            var svgvar = '<svg xmlns="http://www.w3.org/2000/svg" ';
            if (options.classname) {
                svgvar += 'class="' + options.classname + '" ';
                exsign.class = options.classname;
            }
            if (options.size != 'x') {
                svgvar += 'width="' + (w * options.size) + '" height="' + (h * options.size) + '" ';
              

            }
            exsign.width = (w * options.size);
            exsign.height = (h * options.size);
            svgvar += 'viewBox="' + x1 + ' ' + y1 + ' ' + w + ' ' + h + '">';
    
            exsign.text = text;
           
            if (options.view != options.copy) {
                svgvar += '<text style="font-size:0%;">';
                svgvar += options.copy === "code" ? code(text) : options.copy === "uni8" ? uni8(text) : options.copy === "pua" ? pua(text) : text;
                svgvar += '</text>';
            }
            if (options.back) {
               

                svgvar += '  <rect x="' + x1 + '" y="' + y1 + '" width="' + w + '" height="' + h + '" style="fill:' + options.back + ';" />';
            }
            exsign.x = x1;
            exsign.y = y1;
            exsign.width = w;
            exsign.height = h;
            exsign.backfill = options.back;
            svgvar += syms.join('') + "</svg>";
            exsign.syms = exsyms;
            if (options.laned) {
              
                svgvar = '<div style="padding:10px;position:relative;width:' + w + 'px;height:' + h + 'px;left:' + l + 'px;">' + svgvar + '</div>';
            }
            exsign.laned = options.laned ? true : false;
            exsign.left = l;
            } catch (e) {
                alert(e);
            }
            return exsign;
        };
        var canvas = function(text, options) {
            var canvasvar = document.createElement("canvas");
            var fswvar = fsw(text, true);
            var stylingvar = styling(text);
            var keysize;
            if (!fswvar) {
                var keyvar = key(text);
                keysize = size(keyvar);
                if (!keyvar) {
                    return '';
                }
                if (keyvar.length === 6) {
                    fswvar = keyvar + "500x500";
                } else {
                    fswvar = keyvar;
                }
            }
            if (!options) {
                options = {};
            }
            if (options.size) {
                options.size = parseFloat(options.size);
            } else {
                options.size = 1;
            }
            if (options.colorize) {
                options.colorize = true;
            } else {
                options.colorize = false;
            }
            if (options.pad) {
                options.pad = pInt(options.pad);
            } else {
                options.pad = 0;
            }
            if (!options.line) {
                options.line = "black";
            } else {
                options.line = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.line) ? "#" + options.line : options.line;
            }
            if (!options.fill) {
                options.fill = "white";
            } else {
                options.fill = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.fill) ? "#" + options.fill : options.fill;
            }
            if (!options.back) {
                options.back = "";
            } else {
                options.back = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.back) ? "#" + options.back : options.back;
            }
            options.E = [];
            options.F = [];

            if (stylingvar) {
                var rs;
                rs = stylingvar.match(/C/);
                options.colorize = rs ? true : false;

                rs = stylingvar.match(/P[0-9]{2}/);
                if (rs) {
                    options.pad = pInt(rs[0].substring(1, rs[0].length));
                }

                rs = stylingvar.match(/G\(([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+)\)/);
                if (rs) {
                    var back = rs[0].substring(2, rs[0].length - 1);
                    options.back = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(back) ? "#" + back : back;
                }

                var stylings = stylingvar.split('+');
                rs = stylings[0].match(/D\(([0-9a-f]{3}([0-9a-f]{3})?|[a-zA-Z]+)(,([0-9a-f]{3}([0-9a-f]{3})?|[a-zA-Z]+))?\)/);
                if (rs) {
                    var colors1 = rs[0].substring(2, rs[0].length - 1).split(',');
                    if (colors1[0]) {
                        options.line = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors1[0]) ? "#" + colors1[0] : colors1[0];
                    }
                    if (colors1[1]) {
                        options.fill = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors1[1]) ? "#" + colors1[1] : colors1[1];
                    }
                }

                rs = stylings[0].match(/Z[0-9]+(\.[0-9]+)?/);
                if (rs) {
                    options.size = rs[0].substring(1, rs[0].length);
                }

                if (!stylings[1]) {
                    stylings[1] = '';
                }
                rs = stylings[1].match(/D[0-9]{2}\(([0-9a-f]{3}([0-9a-f]{3})?|[a-wyzA-Z]+)(,([0-9a-f]{3}([0-9a-f]{3})?|[a-wyzA-Z]+))?\)/g);
                if (rs) {
                    var i1;
                    for (i1 = 0; i1 < rs.length; i1++) {
                        var pos1 = pInt(rs[i1].substring(1, 3));
                        var colors2 = rs[i1].substring(4, rs[i1].length - 1).split(',');
                        if (colors2[0]) {
                            colors2[0] = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors2[0]) ? "#" + colors2[0] : colors2[0];
                        }
                        if (colors2[1]) {
                            colors2[1] = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors2[1]) ? "#" + colors2[1] : colors2[1];
                        }
                        options.E[pos1] = colors2;
                    }
                }

                rs = stylings[1].match(/Z[0-9]{2},[0-9]+(\.[0-9]+)?(,[0-9]{3}x[0-9]{3})?/g);
                if (rs) {
                    var i2;
                    for (i2 = 0; i2 < rs.length; i2++) {
                        var pos2 = pInt(rs[i2].substring(1, 3));
                        var sizevar2 = rs[i2].substring(4, rs[i2].length).split(',');
                        sizevar2[0] = parseFloat(sizevar2[0]);
                        options.F[pos2] = sizevar2;
                    }
                }
            }


            var r, rsym, rcoord, sym, syms, o;
            r = /(A(S[123][0-9a-f]{2}[0-5][0-9a-f])+)?[BLMR]([0-9]{3}x[0-9]{3})(S[123][0-9a-f]{2}[0-5][0-9a-f][0-9]{3}x[0-9]{3})*|S38[7-9ab][0-5][0-9a-f][0-9]{3}x[0-9]{3}/g;
            rsym = /S[123][0-9a-f]{2}[0-5][0-9a-f][0-9]{3}x[0-9]{3}/g;
            rcoord = /[0-9]{3}x[0-9]{3}/g;
            o = {};
            o.L = -1;
            o.R = 1;
            var x, x1 = 500,
                x2 = 500,
                y, y1 = 500,
                y2 = 500,
                k, w, h;
            k = fswvar.charAt(0);
            var bboxvar = bbox(fswvar);
            bboxvar = bboxvar.split(' ');
            x1 = pInt(bboxvar[0]);
            x2 = pInt(bboxvar[1]);
            y1 = pInt(bboxvar[2]);
            y2 = pInt(bboxvar[3]);

            if (k === 'S') {
                if (x1 === 500 && y1 === 500) {
                    var sizevar3 = keysize.split('x');
                    x2 = 500 + pInt(sizevar3[0]);
                    y2 = 500 + pInt(sizevar3[1]);
                } else {
                    x2 = 1000 - x1;
                    y2 = 1000 - y1;
                }
            }

            syms = fswvar.match(rsym);
            var i3;
            for (i3 = 0; i3 < syms.length; i3++) {
                sym = syms[i3].slice(0, 6);
                x = syms[i3].slice(6, 9);
                y = syms[i3].slice(10, 13);
                if (options.F[i3 + 1]) {
                    if (options.F[i3 + 1][1]) {
                        x = pInt(x) + pInt(options.F[i3 + 1][1].slice(0, 3)) - 500;
                        y = pInt(y) + pInt(options.F[i3 + 1][1].slice(4, 7)) - 500;
                        x1 = Math.min(x1, x);
                        y1 = Math.min(y1, y);
                    }
                    var keysized1 = size(sym);
                    if (keysized1) {
                        keysized1 = keysized1.split('x');
                        x2 = Math.max(x2, pInt(x) + (options.F[i3 + 1][0] * pInt(keysized1[0])));
                        y2 = Math.max(y2, pInt(y) + (options.F[i3 + 1][0] * pInt(keysized1[1])));
                    }

                }
            }

            x1 = x1 - options.pad;
            x2 = x2 + options.pad;
            y1 = y1 - options.pad;
            y2 = y2 + options.pad;
            w = (x2 - x1) * options.size;
            h = (y2 - y1) * options.size;
            canvasvar.width = w;
            canvasvar.height = h;
            var context = canvasvar.getContext("2d");
            if (options.back) {
                context.rect(0, 0, w, h);
                context.fillStyle = options.back;
                context.fill();
                //      context.fillStyle = options.back;
                //      context.fillRect(0,0,w,h);
            }
            syms = fswvar.match(rsym);
            var i4;
            for (i4 = 0; i4 < syms.length; i4++) {
                sym = syms[i4].slice(0, 6);
                x = syms[i4].slice(6, 9);
                y = syms[i4].slice(10, 13);
                if (options.F[i4 + 1]) {
                    if (options.F[i4 + 1][1]) {
                        x = pInt(x) + pInt(options.F[i4 + 1][1].slice(0, 3)) - 500;
                        y = pInt(y) + pInt(options.F[i4 + 1][1].slice(4, 7)) - 500;
                        x1 = Math.min(x1, x);
                        y1 = Math.min(y1, y);
                    }
                    var keysized2 = size(sym);
                    if (keysized2) {
                        keysized2 = keysized2.split('x');
                        x2 = Math.max(x2, pInt(x) + (options.F[i4 + 1][0] * pInt(keysized2[0])));
                        y2 = Math.max(y2, pInt(y) + (options.F[i4 + 1][0] * pInt(keysized2[1])));
                    }

                }
                context.font = (options.F[i4 + 1] ? 30 * options.size * options.F[i4 + 1][0] : 30 * options.size) + "px 'SignWriting 2010 Filling'";
                context.fillStyle = (options.E[i4 + 1] ? options.E[i4 + 1][1] ? options.E[i4 + 1][1] : options.fill : options.fill);
                context.fillText(code(sym), ((x - x1) * options.size), ((y - y1) * options.size));
                context.font = (options.F[i4 + 1] ? 30 * options.size * options.F[i4 + 1][0] : 30 * options.size) + "px 'SignWriting 2010'";
                context.fillStyle = (options.E[i4 + 1] ? options.E[i4 + 1][0] : options.colorize ? '#' + colorize(sym) : options.line);
                context.fillText(code(sym), ((x - x1) * options.size), ((y - y1) * options.size));
            }
            return canvasvar;
        };
        var png = function(fswvar, options) {
            if (fsw(fswvar, true) || key(fswvar, true)) {
                var canvasvar = canvas(fswvar, options);
                var pngvar = canvasvar.toDataURL("image/png");
                canvasvar.remove();
                return pngvar;
            } else {
                return '';
            }
        };
        var query = function(queryvar) {
            queryvar = queryvar.match(/Q((A(S[123][0-9a-f]{2}[0-5u][0-9a-fu]|R[123][0-9a-f]{2}t[123][0-9a-f]{2})+)?T)?((R[123][0-9a-f]{2}t[123][0-9a-f]{2}([0-9]{3}x[0-9]{3})?)|(S[123][0-9a-f]{2}[0-5u][0-9a-fu]([0-9]{3}x[0-9]{3})?))*(V[0-9]+)?/);
            if (queryvar) {
                return queryvar[0];
            } else {
                return '';
            }
        };
        /**
         * @param {string=} hexvar Some value (optional).
         */
        var range = function(min, maxvar, hexvar) {
            var pattern, re, diff, tmax, cnt, minV, maxV;
            if (!hexvar) {
                hexvar = '';
            }
            min = ("000" + min).slice(-3);
            maxvar = '' + maxvar;
            pattern = '';

            if (min === maxvar) {
                return min;
            } //ending pattern will be series of connected OR ranges
            re = [];

            //first pattern+  10's don't match and the min 1's are not zero
            //odd number to 9
            if (!(min[0] === maxvar[0] && min[1] === maxvar[1])) {
                if (min[2] != '0') {
                    pattern = min[0] + min[1];
                    if (hexvar) {
                        //switch for dex
                        switch (min[2]) {
                            case "f":
                                pattern += 'f';
                                break;
                            case "e":
                                pattern += '[ef]';
                                break;
                            case "d":
                            case "c":
                            case "b":
                            case "a":
                                pattern += '[' + min[2] + '-f]';
                                break;
                            default:
                                switch (min[2]) {
                                    case "9":
                                        pattern += '[9a-f]';
                                        break;
                                    case "8":
                                        pattern += '[89a-f]';
                                        break;
                                    default:
                                        pattern += '[' + min[2] + '-9a-f]';
                                        break;
                                }
                                break;
                        }
                        diff = 15 - pInt(min[2], 16) + 1;
                        min = '' + ((pInt(min, 16) + diff)).toString(16);
                        re.push(pattern);
                    } else {
                        //switch for dex
                        switch (min[2]) {
                            case "9":
                                pattern += '9';
                                break;
                            case "8":
                                pattern += '[89]';
                                break;
                            default:
                                pattern += '[' + min[2] + '-9]';
                                break;
                        }
                        diff = 9 - min[2] + 1;
                        min = '' + (min * 1 + diff);
                        re.push(pattern);
                    }
                }
            }
            pattern = '';

            //if hundreds are different, get odd to 99 or ff
            if (min[0] != maxvar[0]) {
                if (min[1] != '0') {
                    if (hexvar) {
                        //scrape to ff
                        pattern = min[0];
                        switch (min[1]) {
                            case "f":
                                pattern += 'f';
                                break;
                            case "e":
                                pattern += '[ef]';
                                break;
                            case "d":
                            case "c":
                            case "b":
                            case "a":
                                pattern += '[' + min[1] + '-f]';
                                break;
                            case "9":
                                pattern += '[9a-f]';
                                break;
                            case "8":
                                pattern += '[89a-f]';
                                break;
                            default:
                                pattern += '[' + min[1] + '-9a-f]';
                                break;
                        }
                        pattern += '[0-9a-f]';
                        diff = 15 - pInt(min[1], 16) + 1;
                        min = '' + (pInt(min, 16) + diff * 16).toString(16);
                        re.push(pattern);
                    } else {
                        //scrape to 99
                        pattern = min[0];
                        diff = 9 - min[1] + 1;
                        switch (min[1]) {
                            case "9":
                                pattern += '9';
                                break;
                            case "8":
                                pattern += '[89]';
                                break;
                            default:
                                pattern += '[' + min[1] + '-9]';
                                break;
                        }
                        pattern += '[0-9]';
                        diff = 9 - min[1] + 1;
                        min = '' + (min * 1 + diff * 10);
                        re.push(pattern);
                    }
                }
            }
            pattern = '';

            //if hundreds are different, get to same
            if (min[0] != maxvar[0]) {
                if (hexvar) {
                    diff = pInt(maxvar[0], 16) - pInt(min[0], 16);
                    tmax = (pInt(min[0], 16) + diff - 1).toString(16);

                    switch (diff) {
                        case 1:
                            pattern = min[0];
                            break;
                        case 2:
                            pattern = '[' + min[0] + tmax + ']';
                            break;
                        default:
                            if (pInt(min[0], 16) > 9) {
                                minV = 'h';
                            } else {
                                minV = 'd';
                            }
                            if (pInt(tmax, 16) > 9) {
                                maxV = 'h';
                            } else {
                                maxV = 'd';
                            }
                            switch (minV + maxV) {
                                case "dd":
                                    pattern += '[' + min[0] + '-' + tmax + ']';
                                    break;
                                case "dh":
                                    diff = 9 - min[0];
                                    //firs get up to 9
                                    switch (diff) {
                                        case 0:
                                            pattern += '[9';
                                            break;
                                        case 1:
                                            pattern += '[89';
                                            break;
                                        default:
                                            pattern += '[' + min[0] + '-9';
                                            break;
                                    }
                                    switch (tmax[0]) {
                                        case 'a':
                                            pattern += 'a]';
                                            break;
                                        case 'b':
                                            pattern += 'ab]';
                                            break;
                                        default:
                                            pattern += 'a-' + tmax + ']';
                                            break;
                                    }
                                    break;
                                case "hh":
                                    pattern += '[' + min[0] + '-' + tmax + ']';
                                    break;
                            }
                    }

                    pattern += '[0-9a-f][0-9a-f]';
                    diff = pInt(maxvar[0], 16) - pInt(min[0], 16);
                    min = '' + (pInt(min, 16) + diff * 256).toString(16);
                    re.push(pattern);
                } else {
                    diff = maxvar[0] - min[0];
                    tmax = min[0] * 1 + diff - 1;

                    switch (diff) {
                        case 1:
                            pattern = min[0];
                            break;
                        case 2:
                            pattern = '[' + min[0] + tmax + ']';
                            break;
                        default:
                            pattern = '[' + min[0] + '-' + tmax + ']';
                            break;
                    }
                    pattern += '[0-9][0-9]';
                    min = '' + (min * 1 + diff * 100);
                    re.push(pattern);
                }
            }
            pattern = '';

            //if tens are different, get to same
            if (min[1] != maxvar[1]) {
                if (hexvar) {
                    diff = pInt(maxvar[1], 16) - pInt(min[1], 16);
                    tmax = (pInt(min[1], 16) + diff - 1).toString(16);
                    pattern = min[0];
                    switch (diff) {
                        case 1:
                            pattern += min[1];
                            break;
                        case 2:
                            pattern += '[' + min[1] + tmax + ']';
                            break;
                        default:

                            if (pInt(min[1], 16) > 9) {
                                minV = 'h';
                            } else {
                                minV = 'd';
                            }
                            if (pInt(tmax, 16) > 9) {
                                maxV = 'h';
                            } else {
                                maxV = 'd';
                            }
                            switch (minV + maxV) {
                                case "dd":
                                    pattern += '[' + min[1];
                                    if (diff > 1) {
                                        pattern += '-';
                                    }
                                    pattern += tmax + ']';
                                    break;
                                case "dh":
                                    diff = 9 - min[1];
                                    //firs get up to 9
                                    switch (diff) {
                                        case 0:
                                            pattern += '[9';
                                            break;
                                        case 1:
                                            pattern += '[89';
                                            break;
                                        default:
                                            pattern += '[' + min[1] + '-9';
                                            break;
                                    }
                                    switch (maxvar[1]) {
                                        case 'a':
                                            pattern += ']';
                                            break;
                                        case 'b':
                                            pattern += 'a]';
                                            break;
                                        default:
                                            pattern += 'a-' + (pInt(maxvar[1], 16) - 1).toString(16) + ']';
                                            break;
                                    }
                                    break;
                                case "hh":
                                    pattern += '[' + min[1];
                                    if (diff > 1) {
                                        pattern += '-';
                                    }
                                    pattern += (pInt(maxvar[1], 16) - 1).toString(16) + ']';
                                    break;
                            }
                            break;
                    }
                    pattern += '[0-9a-f]';
                    diff = pInt(maxvar[1], 16) - pInt(min[1], 16);
                    min = '' + (pInt(min, 16) + diff * 16).toString(16);
                    re.push(pattern);
                } else {
                    diff = maxvar[1] - min[1];
                    tmax = min[1] * 1 + diff - 1;
                    pattern = min[0];
                    switch (diff) {
                        case 1:
                            pattern += min[1];
                            break;
                        case 2:
                            pattern += '[' + min[1] + tmax + ']';
                            break;
                        default:
                            pattern += '[' + min[1] + '-' + tmax + ']';
                            break;
                    }
                    pattern += '[0-9]';
                    min = '' + (min * 1 + diff * 10);
                    re.push(pattern);
                }
            }
            pattern = '';

            //if digits are different, get to same
            if (min[2] != maxvar[2]) {
                if (hexvar) {
                    pattern = min[0] + min[1];
                    diff = pInt(maxvar[2], 16) - pInt(min[2], 16);
                    if (pInt(min[2], 16) > 9) {
                        minV = 'h';
                    } else {
                        minV = 'd';
                    }
                    if (pInt(maxvar[2], 16) > 9) {
                        maxV = 'h';
                    } else {
                        maxV = 'd';
                    }
                    switch (minV + maxV) {
                        case "dd":
                            pattern += '[' + min[2];
                            if (diff > 1) {
                                pattern += '-';
                            }
                            pattern += maxvar[2] + ']';
                            break;
                        case "dh":
                            diff = 9 - min[2];
                            //firs get up to 9
                            switch (diff) {
                                case 0:
                                    pattern += '[9';
                                    break;
                                case 1:
                                    pattern += '[89';
                                    break;
                                default:
                                    pattern += '[' + min[2] + '-9';
                                    break;
                            }
                            switch (maxvar[2]) {
                                case 'a':
                                    pattern += 'a]';
                                    break;
                                case 'b':
                                    pattern += 'ab]';
                                    break;
                                default:
                                    pattern += 'a-' + maxvar[2] + ']';
                                    break;
                            }

                            break;
                        case "hh":
                            pattern += '[' + min[2];
                            if (diff > 1) {
                                pattern += '-';
                            }
                            pattern += maxvar[2] + ']';
                            break;
                    }
                    diff = pInt(maxvar[2], 16) - pInt(min[2], 16);
                    min = '' + (pInt(min, 16) + diff).toString(16);
                    re.push(pattern);
                } else {
                    diff = maxvar[2] - min[2];
                    pattern = min[0] + min[1];
                    switch (diff) {
                        case 0:
                            pattern += min[2];
                            break;
                        case 1:
                            pattern += '[' + min[2] + maxvar[2] + ']';
                            break;
                        default:
                            pattern += '[' + min[2] + '-' + maxvar[2] + ']';
                            break;
                    }
                    min = '' + (min * 1 + diff);
                    re.push(pattern);
                }
            }
            pattern = '';

            //last place is whole hundred
            if (min[2] === '0' && maxvar[2] === '0') {
                pattern = maxvar;
                re.push(pattern);
            }
            pattern = '';

            cnt = re.length;
            if (cnt === 1) {
                pattern = re[0];
            } else {
                pattern = re.join(')|(');
                pattern = '((' + pattern + '))';
            }
            return pattern;
        };
        /**
         * @param {number=} fuzz Some value (optional).
         */
        var regex = function(queryvar, fuzz) {
            queryvar = query(queryvar);
            if (!queryvar) {
                return '';
            }
            var fswPattern, part, from, to, reRange, segment, x, y, base, fillvar, rotatevar;
            if (!fuzz) {
                fuzz = 20;
            }
            var reSym = 'S[123][0-9a-f]{2}[0-5][0-9a-f]';
            var reCoord = '[0-9]{3}x[0-9]{3}';
            var reWord = '[BLMR](' + reCoord + ')(' + reSym + reCoord + ')*';
            var reTerm = '(A(' + reSym + ')+)';
            var qRange = 'R[123][0-9a-f]{2}t[123][0-9a-f]{2}';
            var qSym = 'S[123][0-9a-f]{2}[0-5u][0-9a-fu]';
            var qCoord = '([0-9]{3}x[0-9]{3})?';
            var qVar = '(V[0-9]+)';
            var qTerm;
            queryvar = query(queryvar);
            if (!queryvar) {
                return '';
            }
            if (queryvar === 'Q') {
                return [reTerm + "?" + reWord];
            }
            if (queryvar === 'QT') {
                return [reTerm + reWord];
            }
            var segments = [];
            var term = queryvar.indexOf('T') + 1;
            if (term) {
                qTerm = '(A';
                var qat = queryvar.slice(0, term);
                queryvar = queryvar.replace(qat, '');
                if (qat === 'QT') {
                    qTerm += '(' + reSym + ')+)';
                } else {
                    var matches1 = qat.match(new RegExp('(' + qSym + '|' + qRange + ')', 'g'));
                    if (matches1) {
                        var i1;
                        for (i1 = 0; i1 < matches1.length; i1++) {
                            var matched = matches1[i1].match(new RegExp(qSym));
                            if (matched) {
                                segment = matched[0].slice(0, 4);
                                fillvar = matched[0].slice(4, 5);
                                if (fillvar === 'u') {
                                    segment += '[0-5]';
                                } else {
                                    segment += fillvar;
                                }
                                rotatevar = matched[0].slice(5, 6);
                                if (rotatevar === 'u') {
                                    segment += '[0-9a-f]';
                                } else {
                                    segment += rotatevar;
                                }
                                qTerm += segment;
                            } else {
                                from = matches1[i1].slice(1, 4);
                                to = matches1[i1].slice(5, 8);
                                reRange = range(from, to, 'hex');
                                segment = 'S' + reRange + '[0-5][0-9a-f]';
                                qTerm += segment;
                            }
                        }
                        qTerm += '(' + reSym + ')*)';
                    }
                }
            }
            //get the variance
            var matches2 = queryvar.match(new RegExp(qVar, 'g'));
            if (matches2) {
                fuzz = matches2.toString().slice(1) * 1;
            } //this gets all symbols with or without location
            fswPattern = qSym + qCoord;
            var matches3 = queryvar.match(new RegExp(fswPattern, 'g'));
            if (matches3) {
                var i2;
                for (i2 = 0; i2 < matches3.length; i2++) {
                    part = matches3[i2].toString();
                    base = part.slice(1, 4);
                    segment = 'S' + base;
                    fillvar = part.slice(4, 5);
                    if (fillvar === 'u') {
                        segment += '[0-5]';
                    } else {
                        segment += fillvar;
                    }
                    rotatevar = part.slice(5, 6);
                    if (rotatevar === 'u') {
                        segment += '[0-9a-f]';
                    } else {
                        segment += rotatevar;
                    }
                    if (part.length > 6) {
                        x = part.slice(6, 9) * 1;
                        y = part.slice(10, 13) * 1;
                        //now get the x segment range+++
                        segment += range((x - fuzz), (x + fuzz));
                        segment += 'x';
                        segment += range((y - fuzz), (y + fuzz));
                    } else {
                        segment += reCoord;
                    }
                    //now I have the specific search symbol
                    // add to general ksw word
                    segment = reWord + segment + '(' + reSym + reCoord + ')*';
                    if (term) {
                        segment = qTerm + segment;
                    } else {
                        segment = reTerm + "?" + segment;
                    }
                    segments.push(segment);
                }
            }
            //this gets all ranges
            fswPattern = qRange + qCoord;
            var matches4 = queryvar.match(new RegExp(fswPattern, 'g'));
            if (matches4) {
                var i3;
                for (i3 = 0; i3 < matches4.length; i3++) {
                    part = matches4[i3].toString();
                    from = part.slice(1, 4);
                    to = part.slice(5, 8);
                    reRange = range(from, to, "hex");
                    segment = 'S' + reRange + '[0-5][0-9a-f]';
                    if (part.length > 8) {
                        x = part.slice(8, 11) * 1;
                        y = part.slice(12, 15) * 1;
                        //now get the x segment range+++
                        segment += range((x - fuzz), (x + fuzz));
                        segment += 'x';
                        segment += range((y - fuzz), (y + fuzz));
                    } else {
                        segment += reCoord;
                    }
                    // add to general ksw word
                    segment = reWord + segment + '(' + reSym + reCoord + ')*';
                    if (term) {
                        segment = qTerm + segment;
                    } else {
                        segment = reTerm + "?" + segment;
                    }
                    segments.push(segment);
                }
            }
            if (!segments.length) {
                segments.push(qTerm + reWord);
            }
            return segments;
        };
        var results = function(queryvar, text, lane) {
            if (!text) {
                return [];
            }
            if ("BLMR".indexOf(lane) === -1 || lane.length > 1) {
                lane = '';
            }
            var pattern, matches, parts, words;
            var re = regex(queryvar);
            if (!re) {
                return [];
            }
            var i;
            for (i = 0; i < re.length; i++) {
                pattern = re[i];
                matches = text.match(new RegExp(pattern, 'g'));
                if (matches) {
                    text = matches.join(' ');
                } else {
                    text = '';
                }
            }
            if (text) {
                if (lane) {
                    text = text.replace(/B/g, lane);
                    text = text.replace(/L/g, lane);
                    text = text.replace(/M/g, lane);
                    text = text.replace(/R/g, lane);
                }
                parts = text.split(' ');
                words = parts.filter(function(element) {
                    return element in this ? false : this[element] = true;
                }, {});
            } else {
                words = [];
            }
            return words;
        };
        var lines = function(queryvar, text, lane) {
            if (!text) {
                return [];
            }
            if ("BLMR".indexOf(lane) === -1 || lane.length > 1) {
                lane = '';
            }
            var pattern, matches, parts, words;
            var re = regex(queryvar);
            if (!re) {
                return [];
            }
            var i;
            for (i = 0; i < re.length; i++) {
                pattern = re[i];
                pattern = '^' + pattern + '.*';
                matches = text.match(new RegExp(pattern, 'mg'));
                if (matches) {
                    text = matches.join("\n");
                } else {
                    text = '';
                }
            }
            if (text) {
                if (lane) {
                    text = text.replace(/B/g, lane);
                    text = text.replace(/L/g, lane);
                    text = text.replace(/M/g, lane);
                    text = text.replace(/R/g, lane);
                }
                parts = text.split("\n");
                words = parts.filter(function(element) {
                    return element in this ? false : this[element] = true;
                }, {});
            } else {
                words = [];
            }
            return words;
        };
        var convert = function(fswvar, flags) {
            // e - exact symbol in temporal prefix
            // g - general symbol in temporal prefix
            // E - exact symbol in spatial signbox
            // G - general symbol in spatial signbox
            // L - spatial signbox symbol at location
            var queryvar = '';
            if (fsw(fswvar)) {
                if (/^[eg]?([EG]L?)?$/.test(flags)) {
                    var reBase = 'S[123][0-9a-f]{2}';
                    var reSym = reBase + '[0-5][0-9a-f]';
                    var reCoord = '[0-9]{3}x[0-9]{3}';
                    //var reTerm = '(A(' + reSym + ')+)'; //never used
                    var matches, matched;

                    if (flags.indexOf('e') > -1 || flags.indexOf('g') > -1) {
                        //exact symbols or general symbols in order
                        matches = fswvar.match(new RegExp('A(' + reSym + ')*', 'g'));
                        if (matches) {
                            matched = matches[0];
                            if (flags.indexOf('e') > -1) {
                                queryvar += matched + "T";
                            } else {
                                matches = matched.match(new RegExp(reBase, 'g'));
                                queryvar += "A";
                                var i1;
                                for (i1 = 0; i1 < matches.length; i1++) {
                                    queryvar += matches[i1] + "uu";
                                }
                                queryvar += "T";
                            }
                        }
                    }

                    if (flags.indexOf('E') > -1 || flags.indexOf('G') > -1) {
                        //exact symbols or general symbols in spatial
                        matches = fswvar.match(new RegExp(reSym + reCoord, 'g'));
                        if (matches) {
                            var i2;
                            for (i2 = 0; i2 < matches.length; i2++) {
                                if (flags.indexOf('E') > -1) {
                                    queryvar += matches[i2].slice(0, 6);
                                } else {
                                    queryvar += matches[i2].slice(0, 4) + "uu";
                                }
                                if (flags.indexOf('L') > -1) {
                                    queryvar += matches[i2].slice(6, 13);
                                }
                            }
                        }
                    }
                }
            }
            return queryvar ? "Q" + queryvar : '';
        };

        /**
         * @param {number=} radix Some value (optional).
         */
        var pInt = function(s, radix) {
            return parseInt(s, radix);
        };
        /**
         * A namespace.
         * @const
         */
        var publicApi = {};

        publicApi["key"] = key;

        publicApi["fsw"] = fsw;

        publicApi["styling"] = styling;

        publicApi["mirror"] = mirror;

        publicApi["fill"] = fill;

        publicApi["rotate"] = rotate;

        publicApi["scroll"] = scroll;

        publicApi["structure"] = structure;

        publicApi["type"] = type;

        publicApi["is"] = is;

        publicApi["filter"] = filter;

        publicApi["random"] = random;

        publicApi["colorize"] = colorize;

        publicApi["view"] = view;

        publicApi["svg"] = svg;

        publicApi["symbolsList"] = symbolsList;

        publicApi["code"] = code;

        publicApi["uni8"] = uni8;

        publicApi["pua"] = pua;

        publicApi["bbox"] = bbox;

        publicApi["displace"] = displace;

        publicApi["size"] = size;

        publicApi["max"] = max;

        publicApi["norm"] = norm;

        publicApi["canvas"] = canvas;

        publicApi["png"] = png;

        publicApi["query"] = query;

        publicApi["range"] = range;

        publicApi["regex"] = regex;

        publicApi["results"] = results;

        publicApi["lines"] = lines;

        publicApi["convert"] = convert;


        return publicApi;
    }());