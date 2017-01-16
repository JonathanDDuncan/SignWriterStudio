/**
* SignWriting 2010 JavaScript Library v1.9.1
* https://github.com/Slevinski/sw10js
* Copyright (c) 2007-2016, Stephen E Slevinski Jr
* sw10.js is released under the MIT License.
*/
var sw10 = {
  key: function(text,style){
    var key = text.match(/S[123][0-9a-f]{2}[0-5][0-9a-f]([0-9]{3}x[0-9]{3})?/g);
    if (!key) {
      return '';
    } else {
      return key[0] + (style?sw10.styling(text):'');
    }
  },
  fsw: function(text,style){
    var fsw = text.match(/(A(S[123][0-9a-f]{2}[0-5][0-9a-f])+)?[BLMR]([0-9]{3}x[0-9]{3})(S[123][0-9a-f]{2}[0-5][0-9a-f][0-9]{3}x[0-9]{3})*|S38[7-9ab][0-5][0-9a-f][0-9]{3}x[0-9]{3}/);
    if (!fsw) {
      return '';
    } else {
      return fsw[0] + (style?sw10.styling(text):'');
    }
  },
  styling: function(text){
    var sfsw = text.match(/-C?(P[0-9]{2})?(G_([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+)_)?(D_([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+)(,([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+))?_)?(Z([0-9]+(\.[0-9]+)?|x))?(-(D[0-9]{2}_([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+)(,([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+))?_)*(Z[0-9]{2},[0-9]+(\.[0-9]+)?(,[0-9]{3}x[0-9]{3})?)*)?/);
    if (!sfsw) {
      return '';
    } else {
      return sfsw[0];
    }
  },
  mirror: function(key){
    key = sw10.key(key);
    if (!sw10.size(key)) {return '';}
    var base = key.slice(0,4);
    var fill = key.slice(4,5);
    var rot = parseInt(key.slice(5,6),16);
    var key1 = base + "08";
    var key2 = base + "18";
    var rAdd;
    if (sw10.size(key1) || sw10.size(key2)){
      rAdd = 8;
    } else {
      if ((rot===0) || (rot==4)) {rAdd=0;}
      if ((rot==1) || (rot==5)) {rAdd=6;}
      if ((rot==2) || (rot==6)) {rAdd=4;}
      if ((rot==3) || (rot==7)) {rAdd=2;}
    }
    key='';
    while (!sw10.size(key)) {
      rot += rAdd;
      if ((rot>7) && (rAdd<8)) { rot = rot -8;}
      if (rot>15) { rot = rot -16;}
      key = base + fill + rot.toString(16);
    }
    return key;
  },
  fill: function(key,step){
    key = sw10.key(key);
    if (!sw10.size(key)) {return '';}
    if (step!=-1) {step=1;}
    var base = key.slice(0,4);
    var fill = parseInt(key.slice(4,5));
    var rot = key.slice(5,6);
    key='';
    while (!sw10.size(key)){
      fill += step;
      if (fill>5) {fill=0;}
      if (fill<0) {fill=5;}
      key = base + fill + rot;
    }
    return key;
  },
  rotate: function(key,step){
    key = sw10.key(key);
    if (!sw10.size(key)) {return '';}
    if (step!=-1) {step=1;}
    var base = key.slice(0,4);
    var fill = key.slice(4,5);
    var rot = parseInt(key.slice(5,6),16);
    key='';
    while (!sw10.size(key)){
      if (rot>7){
        rot += step;
        if (rot>15) {rot=8;}
        if (rot<8) {rot=15;}
        key = base + fill + rot.toString(16);
      } else {
        rot -= step;
        if (rot>7) {rot=0;}
        if (rot<0) {rot=7;}
        key = base + fill + rot;
      }
    }
    return key;
  },
  scroll: function(key,step){
    key = sw10.key(key);
    if (!sw10.size(key)) {return '';}
    if (step!=-1) {step=1;}
    var base = parseInt(key.slice(1,4),16) + step;
    var fill = key.slice(4,5);
    var rot = key.slice(5,6);
    var nkey= 'S' + base.toString(16) + fill + rot;
    if(sw10.size(nkey)){
      return nkey;
    } else {
      return key;
    }
  },
  structure: function(division,key,opt){
    var arrs = {kind:['S100','S37f','S387'],
      category:['S100','S205','S2f7','S2ff','S36d','S37f','S387'],
      group:['S100','S10e','S11e','S144','S14c','S186','S1a4','S1ba','S1cd','S1f5','S205','S216','S22a','S255','S265','S288','S2a6','S2b7','S2d5','S2e3','S2f7','S2ff','S30a','S32a','S33b','S359','S36d','S376','S37f','S387']
    };
    var arr = arrs[division];
    if (!arr) {return !key?[]:opt=="is"?false:'';}
    if (!key&&!opt) {return arr;}
    if (!opt) {opt='';}
    var adj;
    switch(opt){
      case 'is':
        return (arr.indexOf(key.slice(0,4))==-1)?false:true;
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
    var i;
    var index = arr.length;
    for(i=0; i<arr.length; i+=1) {
      if(parseInt(key.slice(1,4),16) < parseInt(arr[i].slice(1,4),16)) {
        index = i;
        break;
      }
    }
    index += adj;
    index = index<0?0:index>=arr.length?arr.length-1:index;
    return arr[index];
  },
  type: function(type){
    var start;
    var end;
    switch(type) {
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
    return [start,end];
  },
  is: function(key,type){
    if (key.length==6 && !sw10.size(key)) {return false;}
    var range = sw10.type(type);
    var start = range[0];
    var end = range[1];
    var char = key.slice(1,4);
    return (parseInt(start,16)<=parseInt(char,16) && parseInt(end,16)>=parseInt(char,16));
  },
  filter: function (fsw,type) {
    var range = sw10.type(type);
    var start = range[0];
    var end = range[1];
    var re = 'S' + sw10.range(start,end,1) + '[0-5][0-9a-f][0-9]{3}x[0-9]{3}';
    var matches = fsw.match(new RegExp(re,'g'));
    if (matches){
      return matches.join('');
    } else {
      return '';
    }
  },
  random: function(type) {
    var range = sw10.type(type);
    var start = range[0];
    var end = range[1];
    var rBase = Math.floor(Math.random() * (parseInt(end,16)-parseInt(start,16)+1) + parseInt(start,16));
    var rFill = Math.floor(Math.random() * 6);
    var rRota = Math.floor(Math.random() * 16);
    var key = "S" + rBase.toString(16) + rFill.toString(16) + rRota.toString(16);
    if (sw10.size(key)){
      return key;
    } else {
      return sw10.random(type);
    }
  },
  colorize: function(key) {
    var color = '000000';
    if (sw10.is(key,'hand')) {color = '0000CC';}
    if (sw10.is(key,'movement')) {color = 'CC0000';}
    if (sw10.is(key,'dynamic')) {color = 'FF0099';}
    if (sw10.is(key,'head')) {color = '006600';}
//    if (sw10.is(key,'trunk')) {color = '000000';}
//    if (sw10.is(key,'limb')) {color = '000000';}
    if (sw10.is(key,'location')) {color = '884411';}
    if (sw10.is(key,'punctuation')) {color = 'FF9900';}
    return color;
  },
  view: function(key,fillone) {
    if (!sw10.is(key)) {return '';}
    var prefix = key.slice(0,4);
    if (fillone){
      return prefix + ((sw10.size(prefix + '00'))?'0':'1') +'0';
    } else {
      return prefix + ((sw10.is(prefix,'hand') && !sw10.structure('group',prefix,'is'))?'1':'0') +'0';
    }
  },
  code: function(text,hexval){
    var key;
    var i;
    var fsw = sw10.fsw(text);
    if (fsw){
      var pattern = 'S[123][0-9a-f]{2}[0-5][0-9a-f]';
      var matches = fsw.match(new RegExp(pattern,'g'));
      for(i=0; i<matches.length; i+=1) {
        key = matches[i];
        fsw = fsw.replace(key,sw10.code(key,hexval));
      }
      return fsw;
    }
    key = sw10.key(text);
    if (!key) {return '';}
    var code = 0x100000 + ((parseInt(key.slice(1,4),16) - 256) * 96) + ((parseInt(key.slice(4,5),16))*16) + parseInt(key.slice(5,6),16) + 1;
    return hexval?code.toString(16).toUpperCase():String.fromCharCode(0xD800 + ((code - 0x10000) >> 10), 0xDC00 + ((code - 0x10000) & 0x3FF));
  },
  uni10: function(text,hexval){
    var key;
    var i;
    var fsw = sw10.fsw(text);
    if (fsw){
      var pattern = 'S[123][0-9a-f]{2}[0-5][0-9a-f]';
      var matches = fsw.match(new RegExp(pattern,'g'));
      for(i=0; i<matches.length; i+=1) {
        key = matches[i];
        fsw = fsw.replace(key,sw10.uni10(key,hexval));
      }
      return fsw;
    }
    key = sw10.key(text);
    if (!key) {return '';}
    var code = 0x40000 + ((parseInt(key.slice(1,4),16) - 256) * 96) + ((parseInt(key.slice(4,5),16))*16) + parseInt(key.slice(5,6),16) + 1;
    return hexval?code.toString(16).toUpperCase():String.fromCharCode(0xD800 + ((code - 0x10000) >> 10), 0xDC00 + ((code - 0x10000) & 0x3FF));
  },
  uni8: function(text,hexval){
    var key;
    var i;
    var fsw = sw10.fsw(text);
    if (fsw){
      var pattern = 'S[123][0-9a-f]{2}[0-5][0-9a-f]';
      var matches = fsw.match(new RegExp(pattern,'g'));
      for(i=0; i<matches.length; i+=1) {
        key = matches[i];
        fsw = fsw.replace(key,sw10.uni8(key,hexval));
      }
      return fsw;
    }
    key = sw10.key(text);
    if (!key) {return '';}
    var base = parseInt(key.substr(1,3),16) + parseInt('1D700',16);
    var uni8 = hexval?base.toString(16).toUpperCase():String.fromCharCode(0xD800 + ((base - 0x10000) >> 10), 0xDC00 + ((base - 0x10000) & 0x3FF));
    var fill = key.substr(4,1);
    if (fill!="0"){
      fill = parseInt(fill,16) + parseInt('1DA9A',16);
      uni8 += hexval?fill.toString(16).toUpperCase():String.fromCharCode(0xD800 + ((fill - 0x10000) >> 10), 0xDC00 + ((fill - 0x10000) & 0x3FF));
    }
    var rotation = key.substr(5,1);
    if (rotation!="0"){
      rotation = parseInt(rotation,16) + parseInt('1DAA0',16);
      uni8 += hexval?rotation.toString(16).toUpperCase():String.fromCharCode(0xD800 + ((rotation - 0x10000) >> 10), 0xDC00 + ((rotation - 0x10000) & 0x3FF));
    }
    return uni8;
  },
  pua: function(text,hexval){
    var key;
    var pua;
    var fsw = sw10.fsw(text);
    if (fsw){
      var str;
      var code;
      var coord;
      code = parseInt('FD800',16);
      fsw = fsw.replace('A',hexval?code.toString(16).toUpperCase():String.fromCharCode(0xD800 + (((code) - 0x10000) >> 10), 0xDC00 + (((code) - 0x10000) & 0x3FF)));
      fsw = fsw.replace('B',hexval?(code+1).toString(16).toUpperCase():String.fromCharCode(0xD800 + (((code+1) - 0x10000) >> 10), 0xDC00 + (((code+1) - 0x10000) & 0x3FF)));
      fsw = fsw.replace('L',hexval?(code+2).toString(16).toUpperCase():String.fromCharCode(0xD800 + (((code+2) - 0x10000) >> 10), 0xDC00 + (((code+2) - 0x10000) & 0x3FF)));
      fsw = fsw.replace('M',hexval?(code+3).toString(16).toUpperCase():String.fromCharCode(0xD800 + (((code+3) - 0x10000) >> 10), 0xDC00 + (((code+3) - 0x10000) & 0x3FF)));
      fsw = fsw.replace('R',hexval?(code+4).toString(16).toUpperCase():String.fromCharCode(0xD800 + (((code+4) - 0x10000) >> 10), 0xDC00 + (((code+4) - 0x10000) & 0x3FF)));
      var pattern = '[0-9]{3}x[0-9]{3}';
      var matches = fsw.match(new RegExp(pattern,'g'));
      var i;
      for(i=0; i<matches.length; i+=1) {
        str = matches[i];
        coord = str.split('x');
        coord[0] = parseInt(coord[0]) + parseInt('FDD0C',16);
        coord[1] = parseInt(coord[1]) + parseInt('FDD0C',16);
        pua = hexval?coord[0].toString(16).toUpperCase():String.fromCharCode(0xD800 + ((coord[0] - 0x10000) >> 10), 0xDC00 + ((coord[0] - 0x10000) & 0x3FF));
        pua += hexval?coord[1].toString(16).toUpperCase():String.fromCharCode(0xD800 + ((coord[1] - 0x10000) >> 10), 0xDC00 + ((coord[1] - 0x10000) & 0x3FF));
        fsw = fsw.replace(str,pua);
      }
      pattern = 'S[123][0-9a-f]{2}[0-5][0-9a-f]';
      matches = fsw.match(new RegExp(pattern,'g'));
      for(i=0; i<matches.length; i+=1) {
        key = matches[i];
        fsw = fsw.replace(key,sw10.pua(key,hexval));
      }
      return fsw;
    }
    key = sw10.key(text);
    if (!key) {return '';}
    var base = parseInt(key.substr(1,3),16) + parseInt('FD730',16);
    pua = hexval?base.toString(16).toUpperCase():String.fromCharCode(0xD800 + ((base - 0x10000) >> 10), 0xDC00 + ((base - 0x10000) & 0x3FF));
    var fill = key.substr(4,1);
    fill = parseInt(fill,16) + parseInt('FD810',16);
    pua += hexval?fill.toString(16).toUpperCase():String.fromCharCode(0xD800 + ((fill - 0x10000) >> 10), 0xDC00 + ((fill - 0x10000) & 0x3FF));
    var rotation = key.substr(5,1);
    rotation = parseInt(rotation,16) + parseInt('FD820',16);
    pua += hexval?rotation.toString(16).toUpperCase():String.fromCharCode(0xD800 + ((rotation - 0x10000) >> 10), 0xDC00 + ((rotation - 0x10000) & 0x3FF));
    return pua;
  },
  bbox: function(fsw) {
    var rcoord = /[0-9]{3}x[0-9]{3}/g;
    var x;
    var y;
    var x1;
    var x2;
    var y1;
    var y2;
    var coords = fsw.match(rcoord);
    if (coords){
      var i;
      for (i=0; i < coords.length; i+=1) {
        x = parseInt(coords[i].slice(0, 3));
        y = parseInt(coords[i].slice(4, 7));
        if (i===0){
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
      if (x1==x2 && y1==y2){
        x2 = 1000 - x1;
        y2 = 1000 - y1;
      }
      return '' + x1 + ' ' + x2 + ' ' + y1 + ' ' + y2;
    } else {
      return '';
    }
  },
  displace: function(text,x,y){
    var xpos;
    var ypos;
    var re = '[0-9]{3}x[0-9]{3}';
    var matches = text.match(new RegExp(re,'g'));
    if (matches){
      var i;
      for(i=0; i<matches.length; i+=1) {
        xpos = parseInt(matches[i].slice(0, 3)) + x;
        ypos = parseInt(matches[i].slice(4, 7)) + y;
        text = text.replace(matches[i],xpos + "X" + ypos);
      }
      text = text.replace(/X/g,"x");
    }
    return text;
  },
  sizes:{
  },
  size: function(text) {
    var w;
    var h;
    var s;
    var size;
    var fsw = sw10.fsw(text);
    if (fsw) {
      var bbox = sw10.bbox(fsw);
      bbox = bbox.split(' ');
      var x1 = bbox[0];
      var x2 = bbox[1];
      var y1 = bbox[2];
      var y2 = bbox[3];
      size = (x2-x1) + 'x' + (y2-y1);
      if (size=='0x0') {return '';}
      return size;
    }
    var key = sw10.key(text);
    if (!key) {return '';}
    if (sw10.sizes[key]) {return sw10.sizes[key];}

    var imgData;
    var i;
    var zoom = 2;
    var bound = 76 * zoom;
    if (!sw10.canvaser){
      sw10.canvaser = document.createElement("canvas");
      sw10.canvaser.width = bound;
      sw10.canvaser.height = bound;
    }
    var canvas = sw10.canvaser;
    var context = canvas.getContext("2d");
    context.clearRect(0, 0, bound, bound);
    context.font = (30*zoom) + "px 'SignWriting 2010'";
    context.fillText(sw10.code(key),0,0);
    imgData = context.getImageData(0,0,bound,bound).data;

    wloop:
    for (w=bound-1;w>=0;w--){
      for (h=0;h<bound;h+=1){
        for (s=0;s<4;s+=1){
          i=w*4+(h*4*bound) +s;
          if (imgData[i]){
            break wloop;
          }
        }
      }
    }
    var width = w;
    hloop:
    for (h=bound-1;h>=0;h--){
      for (w=0;w<width;w+=1){
        for (s=0;s<4;s+=1){
          i=w*4+(h*4*bound) +s;
          if (imgData[i]){
            break hloop;
          }
        }
      }
    }
    var height = h+1;
    width= '' + Math.ceil(width/zoom);
    height= '' + Math.ceil(height/zoom);
    // Rounding error in chrome.  Manual fixes.
    if ('S1710d S1711d S1712d S17311 S17321 S17733 S1773f S17743 S1774f S17753 S1775f S16d33 S1713d S1714d S17301 S17329 S1715d'.indexOf(key)>-1){
      width = '20';
    }
    if ('S24c15 S24c30'.indexOf(key)>-1){
      width = '22';
    }
    if ('S2903b'.indexOf(key)>-1){
      width = '23';
    }
    if ('S1d203 S1d233'.indexOf(key)>-1){
      width = '25';
    }
    if ('S24c15'.indexOf(key)>-1){
      width = '28';
    }
    if ('S2e629'.indexOf(key)>-1){
      width = '29';
    }
    if ('S16541 S23425'.indexOf(key)>-1){
      width = '30';
    }
    if ('S2d316'.indexOf(key)>-1){
      width = '40';
    }
    if ('S2541a'.indexOf(key)>-1){
      width = '50';
    }
    if ('S1732f S17731 S17741 S17751'.indexOf(key)>-1){
      height = '20';
    }
    if ('S1412c'.indexOf(key)>-1){
      height = '21';
    }
    if ('S2a903'.indexOf(key)>-1){
      height = '31';
    }
    if ('S2b002'.indexOf(key)>-1){
      height = '36';
    }
    size = width + 'x' + height;
    // Error in chrome.  Manual fix.
    if (size=='0x0') {
      var sizefix = 'S1000815x30 S1000921x30 S1000a30x15 S1000b30x21 S1000c15x30 S1000d21x30 ';
      var ipos = sizefix.indexOf(key);
      if (ipos ==-1) {
        size = '';
      } else {
        var iend = sizefix.indexOf(' ',ipos);
        size = sizefix.slice(ipos + 6,iend);
      }
    } else {
      sw10.sizes[key]=size;
    }
    return size;
  },
  max: function(fsw,type){
    var range = sw10.type(type);
    var start = range[0];
    var end = range[1];
    var re = 'S' + sw10.range(start,end,1) + '[0-5][0-9a-f][0-9]{3}x[0-9]{3}';
    var matches = fsw.match(new RegExp(re,'g'));
    if (matches){
      var key;
      var x;
      var y;
      var size;
      var output='';
      var i;
      for (i=0; i < matches.length; i+=1) {
        key = matches[i].slice(0,6);
        x = parseInt(matches[i].slice(6, 9));
        y = parseInt(matches[i].slice(10, 13));
        size =sw10.size(key).split('x');
        output += key + x + "x" + y + (x+parseInt(size[0])) + 'x' + (y+parseInt(size[1]));
      }
      return output;
    } else {
      return '';
    }
  },
  norm: function (fsw){
    var minx;
    var maxx;
    var miny;
    var maxy;
    var hbox = sw10.bbox(sw10.max(fsw,'hcenter'));
    var vbox = sw10.bbox(sw10.max(fsw,'vcenter'));
    var box = sw10.bbox(sw10.max(fsw));
    if (!box) {return "";}
    if (vbox){
      minx = parseInt(vbox.slice(0,3));
      maxx = parseInt(vbox.slice(4,7));
    } else {
      minx = parseInt(box.slice(0,3));
      maxx = parseInt(box.slice(4,7));
    }
    if (hbox){
      miny = parseInt(hbox.slice(8,11));
      maxy = parseInt(hbox.slice(12,15));
    } else {
      miny = parseInt(box.slice(8,11));
      maxy = parseInt(box.slice(12,15));
    }
    var xcenter = parseInt((minx + maxx)/2);
    var ycenter = parseInt((miny + maxy)/2);
    var xdiff = 500 - xcenter;
    var ydiff = 500 - ycenter;
    var start = fsw.match(/(A(S[123][0-9a-f]{2}[0-5][0-9a-f])+)?[BLMR]/);
    if (!start) {
      start = 'M';
    } else {
      start = start[0];
    }

    fsw = start + parseInt(box.slice(4,7)) + "x" + parseInt(box.slice(12,15)) + sw10.filter(fsw);
    return sw10.displace(fsw,xdiff,ydiff);
  },
  svg: function(text,options){
    var fsw = sw10.fsw(text);
    var styling = sw10.styling(text);
    var stylings;
    var pos;
    var keysize;
    var colors;
    var i;
    var size;
    if (!fsw) {
      var key = sw10.key(text);
      keysize = sw10.size(key);
      if (!keysize) {return '';}
      if (key.length==6) {
        fsw = key + "500x500";
      } else {
        fsw = key;
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
      options.pad = parseInt(options.pad);
    } else {
      options.pad = 0;
    }
    if (!options.line){
      options.line="black";
    } else {
      options.line = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.line)?"#"+options.line:options.line;
    }
    if (!options.fill){
      options.fill="white";
    } else {
      options.fill = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.fill)?"#"+options.fill:options.fill;
    }
    if (!options.back){
      options.back="";
    } else {
      options.back = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.back)?"#"+options.back:options.back;
    }
    options.E = [];
    options.F = [];

    options.view = options.view=="key"?"key":options.view=="uni8"?"uni8":options.view=="pua"?"pua":options.view=="uni10"?"uni10":"code";
    options.copy = options.copy=="code"?"code":options.copy=="uni8"?"uni8":options.copy=="pua"?"pua":options.copy=="uni10"?"uni10":"key";

    if (styling){
      var rs;
      rs = styling.match(/C/);
      options.colorize = rs?true:false;

      rs = styling.match(/P[0-9]{2}/);
      if (rs){
        options.pad = parseInt(rs[0].substring(1,rs[0].length));
      }

      rs = styling.match(/G_([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+)_/);
      if (rs){
        var back = rs[0].substring(2,rs[0].length-1);
        options.back = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(back)?"#"+back:back;
      }
//fix
      stylings = styling.split('-');
      rs = stylings[1].match(/D_([0-9a-f]{3}([0-9a-f]{3})?|[a-zA-Z]+)(,([0-9a-f]{3}([0-9a-f]{3})?|[a-zA-Z]+))?_/);
      if (rs) {
        colors = rs[0].substring(2,rs[0].length-1).split(',');
        if (colors[0]) {
          options.line = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors[0])?"#"+colors[0]:colors[0];
        }
        if (colors[1]) {
          options.fill = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors[1])?"#"+colors[1]:colors[1];
        }
      }

      rs = stylings[1].match(/Z([0-9]+(\.[0-9]+)?|x)/);
      if (rs){
        options.size = parseFloat(rs[0].substring(1,rs[0].length)) || 'x';
      }

      if (!stylings[2]) {
        stylings[2]='';
      }

      rs = stylings[2].match(/D[0-9]{2}_([0-9a-f]{3}([0-9a-f]{3})?|[a-wyzA-Z]+)(,([0-9a-f]{3}([0-9a-f]{3})?|[a-wyzA-Z]+))?_/g);
      if (rs) {
        for (i=0; i < rs.length; i+=1) {
          pos = parseInt(rs[i].substring(1,3));
          colors = rs[i].substring(4,rs[i].length-1).split(',');
          if (colors[0]) {
            colors[0] = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors[0])?"#"+colors[0]:colors[0];
          }
          if (colors[1]) {
            colors[1] = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors[1])?"#"+colors[1]:colors[1];
          }
          options.E[pos]=colors;
        }
      }

      rs = stylings[2].match(/Z[0-9]{2},[0-9]+(\.[0-9]+)?(,[0-9]{3}x[0-9]{3})?/g);
      if (rs){
        for (i=0; i < rs.length; i+=1) {
          pos = parseInt(rs[i].substring(1,3));
          size = rs[i].substring(4,rs[i].length).split(',');
          size[0]=parseFloat(size[0]);
          options.F[pos]=size;
        }
      }
    }

    var sym;
    var syms;
    var gelem;
    var rsym = /S[123][0-9a-f]{2}[0-5][0-9a-f][0-9]{3}x[0-9]{3}/g;
    var o = {};
    o.L = -1;
    o.R = 1;
    var x;
    var x1 = 500;
    var x2 = 500;
    var y;
    var y1 = 500;
    var y2 = 500;
    var k;
    var w;
    var h;
    var l;
    k = fsw.charAt(0);
    var bbox = sw10.bbox(fsw);
    bbox = bbox.split(' ');
    x1 = parseInt(bbox[0]);
    x2 = parseInt(bbox[1]);
    y1 = parseInt(bbox[2]);
    y2 = parseInt(bbox[3]);
    if (k == 'S') {
      if (x1==500 && y1==500){
        size = keysize.split('x');
        x2 = 500 + parseInt(size[0]);
        y2 = 500 + parseInt(size[1]);
      } else {
        x2 = 1000-x1;
        y2 = 1000-y1;
      }
    }
    syms = fsw.match(rsym);
    if (!syms) syms=[];
    var keysized;
    for (i=0; i < syms.length; i+=1) {
      sym = syms[i].slice(0,6);
      x = syms[i].slice(6, 9);
      y = syms[i].slice(10, 13);
      if (options.F[i+1]){
        if (options.F[i+1][1]){
          x = parseInt(x) + parseInt(options.F[i+1][1].slice(0,3))-500;
          y = parseInt(y) + parseInt(options.F[i+1][1].slice(4,7))-500;
          x1 = Math.min(x1,x);
          y1 = Math.min(y1,y);
        }
        keysized = sw10.size(sym);
        if (keysized) {
          keysized = keysized.split('x');
          x2 = Math.max(x2,parseInt(x) + (options.F[i+1][0] * parseInt(keysized[0])));
          y2 = Math.max(y2,parseInt(y) + (options.F[i+1][0] * parseInt(keysized[1])));
        }

      }
      gelem = '<g transform="translate(' + x + ',' + y + ')">';
      gelem += '<text ';
      gelem += 'class="sym-fill" ';
      if (!options.css) {
        gelem += 'style="pointer-events:none;font-family:\'SignWriting 2010 Filling\';font-size:' + (options.F[i+1]?30*options.F[i+1][0]:30) + 'px;fill:' + (options.E[i+1]?options.E[i+1][1]?options.E[i+1][1]:options.fill:options.fill) + ';';
        gelem += options.view=='code'?'':'-webkit-font-feature-settings:\'liga\';font-feature-settings:\'liga\';';
        gelem += '"';
        //-moz-font-feature-settings:'liga';
      }
      gelem += '>';
      gelem += options.view=="key"?sym:options.view=="uni8"?sw10.uni8(sym):options.view=="pua"?sw10.pua(sym):options.view=="uni10"?sw10.uni10(sym):sw10.code(sym);
      gelem += '</text>';
      gelem += '<text ';
      gelem += 'class="sym-line" ';
      if (!options.css) {
        gelem += 'style="';
        gelem += options.view==options.copy?'':'pointer-events:none;';
        gelem += 'font-family:\'SignWriting 2010\';font-size:' + (options.F[i+1]?30*options.F[i+1][0]:30) + 'px;fill:' + (options.E[i+1]?options.E[i+1][0]:options.colorize?'#'+sw10.colorize(sym):options.line) + ';';
        gelem += options.view=='code'?'':'-webkit-font-feature-settings:\'liga\';font-feature-settings:\'liga\';';
        gelem += '"';
      }
      gelem += '>';
      gelem += options.view=="key"?sym:options.view=="uni8"?sw10.uni8(sym):options.view=="pua"?sw10.pua(sym):options.view=="uni10"?sw10.uni10(sym):sw10.code(sym);
      gelem += '</text>';
      gelem += '</g>';
      syms[i] = gelem;
    }

    x1 = x1 - options.pad;
    x2 = x2 + options.pad;
    y1 = y1 - options.pad;
    y2 = y2 + options.pad;
    w = x2 - x1;
    h = y2 - y1;
    l = o[k] || 0;
    l = l * 75 + x1 - 400;
    var svg = '<svg xmlns="http://www.w3.org/2000/svg" ';
    if (options.class) {
      svg += 'class="' + options.class + '" ';
    }
    if (options.size!='x') {
      svg += 'width="' + (w * options.size) + '" height="' + (h * options.size) + '" ';
    }
    svg += 'viewBox="' + x1 + ' ' + y1 + ' ' + w + ' ' + h + '">';
    if (options.view!=options.copy) {
      svg += '<text style="font-size:0%;">';
      svg += options.copy=="code"?sw10.code(text):options.copy=="uni8"?sw10.uni8(text):options.copy=="pua"?sw10.pua(text):options.copy=="uni10"?sw10.uni10(text):text;
      svg += '</text>';
    }
    if (options.back) {
      svg += '  <rect x="' + x1 + '" y="' + y1 + '" width="' + w + '" height="' + h + '" style="fill:' + options.back + ';" />';
    }
    svg += syms.join('') + "</svg>";
    if (options.laned){
      svg = '<div style="padding:10px;position:relative;width:' + w + 'px;height:' + h + 'px;left:' + l + 'px;">' + svg + '</div>';
    }
    return svg;
  },
  symbolsList: function (text, options) {
      var fsw = sw10.fsw(text);
      var styling = sw10.styling(text);
      var stylings;
      var pos;
      var keysize;
      var colors;
      var i;
      var size;
      if (!fsw) {
          var key = sw10.key(text);
          keysize = sw10.size(key);
          if (!keysize) { return ''; }
          if (key.length == 6) {
              fsw = key + "500x500";
          } else {
              fsw = key;
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
          options.pad = parseInt(options.pad);
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

      options.view = options.view == "key" ? "key" : options.view == "uni8" ? "uni8" : options.view == "pua" ? "pua" : options.view == "uni10" ? "uni10" : "code";
      options.copy = options.copy == "code" ? "code" : options.copy == "uni8" ? "uni8" : options.copy == "pua" ? "pua" : options.copy == "uni10" ? "uni10" : "key";

      if (styling) {
          var rs;
          rs = styling.match(/C/);
          options.colorize = rs ? true : false;

          rs = styling.match(/P[0-9]{2}/);
          if (rs) {
              options.pad = parseInt(rs[0].substring(1, rs[0].length));
          }

          rs = styling.match(/G_([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+)_/);
          if (rs) {
              var back = rs[0].substring(2, rs[0].length - 1);
              options.back = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(back) ? "#" + back : back;
          }
          //fix
          stylings = styling.split('-');
          rs = stylings[1].match(/D_([0-9a-f]{3}([0-9a-f]{3})?|[a-zA-Z]+)(,([0-9a-f]{3}([0-9a-f]{3})?|[a-zA-Z]+))?_/);
          if (rs) {
              colors = rs[0].substring(2, rs[0].length - 1).split(',');
              if (colors[0]) {
                  options.line = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors[0]) ? "#" + colors[0] : colors[0];
              }
              if (colors[1]) {
                  options.fill = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors[1]) ? "#" + colors[1] : colors[1];
              }
          }

          rs = stylings[1].match(/Z([0-9]+(\.[0-9]+)?|x)/);
          if (rs) {
              options.size = parseFloat(rs[0].substring(1, rs[0].length)) || 'x';
          }

          if (!stylings[2]) {
              stylings[2] = '';
          }

          rs = stylings[2].match(/D[0-9]{2}_([0-9a-f]{3}([0-9a-f]{3})?|[a-wyzA-Z]+)(,([0-9a-f]{3}([0-9a-f]{3})?|[a-wyzA-Z]+))?_/g);
          if (rs) {
              for (i = 0; i < rs.length; i += 1) {
                  pos = parseInt(rs[i].substring(1, 3));
                  colors = rs[i].substring(4, rs[i].length - 1).split(',');
                  if (colors[0]) {
                      colors[0] = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors[0]) ? "#" + colors[0] : colors[0];
                  }
                  if (colors[1]) {
                      colors[1] = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors[1]) ? "#" + colors[1] : colors[1];
                  }
                  options.E[pos] = colors;
              }
          }

          rs = stylings[2].match(/Z[0-9]{2},[0-9]+(\.[0-9]+)?(,[0-9]{3}x[0-9]{3})?/g);
          if (rs) {
              for (i = 0; i < rs.length; i += 1) {
                  pos = parseInt(rs[i].substring(1, 3));
                  size = rs[i].substring(4, rs[i].length).split(',');
                  size[0] = parseFloat(size[0]);
                  options.F[pos] = size;
              }
          }
      }

      var sym;
      var syms;
      var gelem;
      var exsyms, exsym;
      var rsym = /S[123][0-9a-f]{2}[0-5][0-9a-f][0-9]{3}x[0-9]{3}/g;
      var o = {};
      o.L = -1;
      o.R = 1;
      var x;
      var x1 = 500;
      var x2 = 500;
      var y;
      var y1 = 500;
      var y2 = 500;
      var k;
      var w;
      var h;
      var l;
      k = fsw.charAt(0);
      var bbox = sw10.bbox(fsw);
      bbox = bbox.split(' ');
      x1 = parseInt(bbox[0]);
      x2 = parseInt(bbox[1]);
      y1 = parseInt(bbox[2]);
      y2 = parseInt(bbox[3]);
      if (k == 'S') {
          if (x1 == 500 && y1 == 500) {
              size = keysize.split('x');
              x2 = 500 + parseInt(size[0]);
              y2 = 500 + parseInt(size[1]);
          } else {
              x2 = 1000 - x1;
              y2 = 1000 - y1;
          }
      }
      syms = fsw.match(rsym);
      if (!syms) syms = [];
      var keysized;
      exsyms = [];
      for (i = 0; i < syms.length; i += 1) {
          sym = syms[i].slice(0, 6);
          x = syms[i].slice(6, 9);
          y = syms[i].slice(10, 13);
          if (options.F[i + 1]) {
              if (options.F[i + 1][1]) {
                  x = parseInt(x) + parseInt(options.F[i + 1][1].slice(0, 3)) - 500;
                  y = parseInt(y) + parseInt(options.F[i + 1][1].slice(4, 7)) - 500;
                  x1 = Math.min(x1, x);
                  y1 = Math.min(y1, y);
              }
              keysized = sw10.size(sym);
              if (keysized) {
                  keysized = keysized.split('x');
                  x2 = Math.max(x2, parseInt(x) + (options.F[i + 1][0] * parseInt(keysized[0])));
                  y2 = Math.max(y2, parseInt(y) + (options.F[i + 1][0] * parseInt(keysized[1])));
              }

          }
          exsym = new Object;
          exsym.x = parseInt(x);
          exsym.y = parseInt(y);
          var keysized1 = this.size(sym);

          keysized1 = keysized1.split('x');
          exsym.width = parseInt(keysized1[0]);
          exsym.height = parseInt(keysized1[1]);

          gelem = '<g transform="translate(' + x + ',' + y + ')">';
          gelem += '<text ';
          gelem += 'class="sym-fill" ';
          if (!options.css) {
              exsym.fontsize = Math.round(options.F[i + 1] ? 30 * options.F[i + 1][0] : 30);
              exsym.nwcolor = (options.E[i + 1] ? options.E[i + 1][1] ? options.E[i + 1][1] : options.fill : options.fill);

              gelem += 'style="pointer-events:none;font-family:\'SignWriting 2010 Filling\';font-size:' + (options.F[i + 1] ? 30 * options.F[i + 1][0] : 30) + 'px;fill:' + (options.E[i + 1] ? options.E[i + 1][1] ? options.E[i + 1][1] : options.fill : options.fill) + ';';
              gelem += options.view == 'code' ? '' : '-webkit-font-feature-settings:\'liga\';font-feature-settings:\'liga\';';
              gelem += '"';
              //-moz-font-feature-settings:'liga';
          }
          gelem += '>';
          exsym.size = options.F[i + 1] ? options.F[i + 1][0] : 1;
          exsym.pua = this.pua(sym);
          exsym.code = this.code(sym).codePointAt();
          exsym.key = sym;
          gelem += options.view == "key" ? sym : options.view == "uni8" ? sw10.uni8(sym) : options.view == "pua" ? sw10.pua(sym) : options.view == "uni10" ? sw10.uni10(sym) : sw10.code(sym);
          gelem += '</text>';
          gelem += '<text ';
          gelem += 'class="sym-line" ';
          if (!options.css) {
              gelem += 'style="';
              gelem += options.view == options.copy ? '' : 'pointer-events:none;';
              exsym.nbcolor = (options.E[i + 1] ? options.E[i + 1][0] : options.colorize ? '#' + colorize(sym) : options.line);
              gelem += 'font-family:\'SignWriting 2010\';font-size:' + (options.F[i + 1] ? 30 * options.F[i + 1][0] : 30) + 'px;fill:' + (options.E[i + 1] ? options.E[i + 1][0] : options.colorize ? '#' + sw10.colorize(sym) : options.line) + ';';
              gelem += options.view == 'code' ? '' : '-webkit-font-feature-settings:\'liga\';font-feature-settings:\'liga\';';
              gelem += '"';
          }
          gelem += '>';
          gelem += options.view == "key" ? sym : options.view == "uni8" ? sw10.uni8(sym) : options.view == "pua" ? sw10.pua(sym) : options.view == "uni10" ? sw10.uni10(sym) : sw10.code(sym);
          gelem += '</text>';
          gelem += '</g>';
          syms[i] = gelem;
          exsyms[i] = exsym;
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
      var svg = '<svg xmlns="http://www.w3.org/2000/svg" ';
      if (options.class) {
          svg += 'class="' + options.class + '" ';
          exsign.class = options.classname;
      }
      if (options.size != 'x') {
          svg += 'width="' + (w * options.size) + '" height="' + (h * options.size) + '" ';
      }
      exsign.width = (w * options.size);
      exsign.height = (h * options.size);
      svg += 'viewBox="' + x1 + ' ' + y1 + ' ' + w + ' ' + h + '">';
      exsign.text = text;
      if (options.view != options.copy) {
          svg += '<text style="font-size:0%;">';
          svg += options.copy == "code" ? sw10.code(text) : options.copy == "uni8" ? sw10.uni8(text) : options.copy == "pua" ? sw10.pua(text) : options.copy == "uni10" ? sw10.uni10(text) : text;
          svg += '</text>';
      }
      if (options.back) {
          svg += '  <rect x="' + x1 + '" y="' + y1 + '" width="' + w + '" height="' + h + '" style="fill:' + options.back + ';" />';
      }
      exsign.x = x1;
      exsign.y = y1;
      exsign.width = w;
      exsign.height = h;
      exsign.backfill = options.back;
      svg += syms.join('') + "</svg>";
      exsign.syms = exsyms;
      if (options.laned) {
          svg = '<div style="padding:10px;position:relative;width:' + w + 'px;height:' + h + 'px;left:' + l + 'px;">' + svg + '</div>';
      }
      exsign.laned = options.laned ? true : false;
      exsign.left = l;
      return exsign;
  },
  canvas: function (text, options) {
    var canvas = document.createElement("canvas");
    var fsw = sw10.fsw(text,true);
    var styling = sw10.styling(text);
    var stylings;
    var colors;
    var keysize;
    var keysized;
    var size;
    var i;
    var pos;
    if (!fsw) {
      var key = sw10.key(text);
      keysize=sw10.size(key);
      if (!key) {return '';}
      if (key.length==6) {
        fsw = key + "500x500";
      } else {
        fsw = key;
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
      options.pad = parseInt(options.pad);
    } else {
      options.pad = 0;
    }
    if (!options.line){
      options.line="black";
    } else {
      options.line = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.line)?"#"+options.line:options.line;
    }
    if (!options.fill){
      options.fill="white";
    } else {
      options.fill = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.fill)?"#"+options.fill:options.fill;
    }
    if (!options.back){
      options.back="";
    } else {
      options.back = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(options.back)?"#"+options.back:options.back;
    }
    options.E = [];
    options.F = [];

    if (styling){
      var rs;
      rs = styling.match(/C/);
      options.colorize = rs?true:false;

      rs = styling.match(/P[0-9]{2}/);
      if (rs){
        options.pad = parseInt(rs[0].substring(1,rs[0].length));
      }

      rs = styling.match(/G_([0-9a-fA-F]{3}([0-9a-fA-F]{3})?|[a-zA-Z]+)_/);
      if (rs){
        var back = rs[0].substring(2,rs[0].length-1);
        options.back = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(back)?"#"+back:back;
      }
//fix
      stylings = styling.split('-');
      rs = stylings[1].match(/D_([0-9a-f]{3}([0-9a-f]{3})?|[a-zA-Z]+)(,([0-9a-f]{3}([0-9a-f]{3})?|[a-zA-Z]+))?_/);
      if (rs) {
        colors = rs[0].substring(2,rs[0].length-1).split(',');
        if (colors[0]) {
          options.line = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors[0])?"#"+colors[0]:colors[0];
        }
        if (colors[1]) {
          options.fill = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors[1])?"#"+colors[1]:colors[1];
        }
      }

      rs = stylings[1].match(/Z[0-9]+(\.[0-9]+)?/);
      if (rs){
        options.size = rs[0].substring(1,rs[0].length);
      }

      if (!stylings[2]) {
        stylings[2]='';
      }

      rs = stylings[2].match(/D[0-9]{2}_([0-9a-f]{3}([0-9a-f]{3})?|[a-wyzA-Z]+)(,([0-9a-f]{3}([0-9a-f]{3})?|[a-wyzA-Z]+))?_/g);
      if (rs) {
        for (i=0; i < rs.length; i+=1) {
          pos = parseInt(rs[i].substring(1,3));
          colors = rs[i].substring(4,rs[i].length-1).split(',');
          if (colors[0]) {
            colors[0] = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors[0])?"#"+colors[0]:colors[0];
          }
          if (colors[1]) {
            colors[1] = /^[0-9a-fA-F]{3}([0-9a-fA-F]{3})?$/g.test(colors[1])?"#"+colors[1]:colors[1];
          }
          options.E[pos]=colors;
        }
      }

      rs = stylings[2].match(/Z[0-9]{2},[0-9]+(\.[0-9]+)?(,[0-9]{3}x[0-9]{3})?/g);
      if (rs){
        for (i=0; i < rs.length; i+=1) {
          pos = parseInt(rs[i].substring(1,3));
          size = rs[i].substring(4,rs[i].length).split(',');
          size[0]=parseFloat(size[0]);
          options.F[pos]=size;
        }
      }
    }


    var sym;
    var syms;
    var rsym = /S[123][0-9a-f]{2}[0-5][0-9a-f][0-9]{3}x[0-9]{3}/g;
    var o = {};
    o.L = -1;
    o.R = 1;
    var x;
    var x1 = 500;
    var x2 = 500;
    var y;
    var y1 = 500;
    var y2 = 500;
    var k;
    var w;
    var h;
    k = fsw.charAt(0);
    var bbox = sw10.bbox(fsw);
    bbox = bbox.split(' ');
    x1 = parseInt(bbox[0]);
    x2 = parseInt(bbox[1]);
    y1 = parseInt(bbox[2]);
    y2 = parseInt(bbox[3]);

    if (k == 'S') {
      if (x1==500 && y1==500){
        size = keysize.split('x');
        x2 = 500 + parseInt(size[0]);
        y2 = 500 + parseInt(size[1]);
      } else {
        x2 = 1000-x1;
        y2 = 1000-y1;
      }
    }

    syms = fsw.match(rsym);
    for (i=0; i < syms.length; i+=1) {
      sym = syms[i].slice(0,6);
      x = syms[i].slice(6, 9);
      y = syms[i].slice(10, 13);
      if (options.F[i+1]){
        if (options.F[i+1][1]){
          x = parseInt(x) + parseInt(options.F[i+1][1].slice(0,3))-500;
          y = parseInt(y) + parseInt(options.F[i+1][1].slice(4,7))-500;
          x1 = Math.min(x1,x);
          y1 = Math.min(y1,y);
        }
        keysized = sw10.size(sym);
        if (keysized) {
          keysized = keysized.split('x');
          x2 = Math.max(x2,parseInt(x) + (options.F[i+1][0] * parseInt(keysized[0])));
          y2 = Math.max(y2,parseInt(y) + (options.F[i+1][0] * parseInt(keysized[1])));
        }

      }
    }

    x1 = x1 - options.pad;
    x2 = x2 + options.pad;
    y1 = y1 - options.pad;
    y2 = y2 + options.pad;
    w = (x2-x1) * options.size;
    h = (y2-y1) * options.size;
    canvas.width = w;
    canvas.height = h;
    var context = canvas.getContext("2d");
    if (options.back){
      context.rect(0,0,w,h);
      context.fillStyle=options.back;
      context.fill();
//      context.fillStyle = options.back;
//      context.fillRect(0,0,w,h);
    }
    syms = fsw.match(rsym);
    for (i=0; i < syms.length; i+=1) {
      sym = syms[i].slice(0,6);
      x = syms[i].slice(6, 9);
      y = syms[i].slice(10, 13);
      if (options.F[i+1]){
        if (options.F[i+1][1]){
          x = parseInt(x) + parseInt(options.F[i+1][1].slice(0,3))-500;
          y = parseInt(y) + parseInt(options.F[i+1][1].slice(4,7))-500;
          x1 = Math.min(x1,x);
          y1 = Math.min(y1,y);
        }
        keysized = sw10.size(sym);
        if (keysized) {
          keysized = keysized.split('x');
          x2 = Math.max(x2,parseInt(x) + (options.F[i+1][0] * parseInt(keysized[0])));
          y2 = Math.max(y2,parseInt(y) + (options.F[i+1][0] * parseInt(keysized[1])));
        }

      }
      context.font = (options.F[i+1]?30*options.size*options.F[i+1][0]:30*options.size) + "px 'SignWriting 2010 Filling'";
      context.fillStyle =  (options.E[i+1]?options.E[i+1][1]?options.E[i+1][1]:options.fill:options.fill);
      context.fillText(sw10.code(sym),((x-x1)*options.size),((y-y1)*options.size));
      context.font = (options.F[i+1]?30*options.size*options.F[i+1][0]:30*options.size) + "px 'SignWriting 2010'";
      context.fillStyle = (options.E[i+1]?options.E[i+1][0]:options.colorize?'#'+sw10.colorize(sym):options.line);
      context.fillText(sw10.code(sym),((x-x1)*options.size),((y-y1)*options.size));
    }
    return canvas;
  },
  png: function (fsw,options){
    if (sw10.fsw(fsw,true) || sw10.key(fsw,true) ){
      var canvas = sw10.canvas(fsw,options);
      var png = canvas.toDataURL("image/png");
      canvas.remove();
      return png;
    } else {
      return '';
    }
  },
  query: function (query){
    query = query.match(/Q((A(S[123][0-9a-f]{2}[0-5u][0-9a-fu]|R[123][0-9a-f]{2}t[123][0-9a-f]{2})+)?T)?((R[123][0-9a-f]{2}t[123][0-9a-f]{2}([0-9]{3}x[0-9]{3})?)|(S[123][0-9a-f]{2}[0-5u][0-9a-fu]([0-9]{3}x[0-9]{3})?))*(V[0-9]+)?/);
    if (query) {
      return query[0];
    } else {
      return '';
    }
  },
  range: function (min,max,hex){
    var pattern;
    var re;
    var diff;
    var tmax;
    var cnt;
    var minV;
    var maxV;
    if (!hex) {
      hex='';
    }
    min = ("000" + min).slice(-3);
    max = '' + max;
    pattern='';

    if (min===max) {return min;}

    //ending pattern will be series of connected OR ranges
    re = [];

    //first pattern+  10's don't match and the min 1's are not zero
    //odd number to 9
    if (!(min[0]==max[0] && min[1]==max[1])) {
      if (min[2]!='0'){
        pattern = min[0] + min[1];
        if (hex) {
          //switch for dex
          switch (min[2]){
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
            switch (min[2]){
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
          diff = 15-parseInt(min[2],16) +1;
          min = '' + ((parseInt(min,16)+diff)).toString(16);
          re.push(pattern);
        } else {
          //switch for dex
          switch (min[2]){
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
          diff = 9-min[2] +1;
          min = '' + (min*1 + diff);
          re.push(pattern);
        }
      }
    }
    pattern = '';

    //if hundreds are different, get odd to 99 or ff
    if (min[0]!=max[0]){
      if (min[1]!='0'){
        if (hex){
          //scrape to ff
          pattern = min[0];
          switch (min[1]){
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
          diff = 15-parseInt(min[1],16) +1;
          min = '' + (parseInt(min,16)+diff*16).toString(16);
          re.push(pattern);
        } else {
          //scrape to 99
          pattern = min[0];
          diff = 9-min[1] +1;
          switch (min[1]){
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
          diff = 9-min[1] +1;
          min = '' + (min*1 + diff*10);
          re.push(pattern);
        }
      }
    }
    pattern = '';

    //if hundreds are different, get to same
    if (min[0]!=max[0]){
      if (hex){
        diff = parseInt(max[0],16) - parseInt(min[0],16);
        tmax = (parseInt(min[0],16) + diff-1).toString(16);

        switch (diff){
        case 1:
          pattern = min[0];
          break;
        case 2:
          pattern = '[' + min[0] + tmax + ']';
          break;
        default:
          if (parseInt(min[0],16)>9){
            minV = 'h';
          } else {
            minV = 'd';
          }
          if (parseInt(tmax,16)>9){
            maxV = 'h';
          } else {
            maxV = 'd';
          }
          switch (minV + maxV){
          case "dd":
            pattern += '[' + min[0] + '-' + tmax + ']';
            break;
          case "dh":
            diff = 9 - min[0];
            //firs get up to 9
            switch (diff){
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
            switch (tmax[0]){
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
        diff = parseInt(max[0],16) - parseInt(min[0],16);
        min = '' + (parseInt(min,16)+diff*256).toString(16);
        re.push(pattern);
      } else {
        diff = max[0] - min[0];
        tmax = min[0]*1 + diff-1;

        switch (diff){
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
        min = '' + (min*1 + diff*100);
        re.push(pattern);
      }
    }
    pattern = '';

    //if tens are different, get to same
    if (min[1]!=max[1]){
      if (hex){
        diff = parseInt(max[1],16) - parseInt(min[1],16);
        tmax = (parseInt(min[1],16) + diff-1).toString(16);
        pattern = min[0];
        switch (diff){
        case 1:
          pattern += min[1];
          break;
        case 2:
          pattern += '[' + min[1] + tmax + ']';
          break;
        default:

          if (parseInt(min[1],16)>9){
            minV = 'h';
          } else {
            minV = 'd';
          }
          if (parseInt(tmax,16)>9){
            maxV = 'h';
          } else {
            maxV = 'd';
          }
          switch (minV + maxV){
          case "dd":
            pattern += '[' + min[1];
            if (diff>1) {
              pattern += '-';
            }
            pattern += tmax + ']';
            break;
          case "dh":
            diff = 9 - min[1];
            //firs get up to 9
            switch (diff){
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
            switch (max[1]){
            case 'a':
              pattern += ']';
              break;
            case 'b':
              pattern += 'a]';
              break;
            default:
              pattern += 'a-' + (parseInt(max[1],16)-1).toString(16) + ']';
              break;
            }
            break;
          case "hh":
            pattern += '[' + min[1];
            if (diff>1) {
              pattern += '-';
            }
            pattern += (parseInt(max[1],16)-1).toString(16) + ']';
            break;
          }
          break;
        }
        pattern += '[0-9a-f]';
        diff = parseInt(max[1],16) - parseInt(min[1],16);
        min = '' + (parseInt(min,16)+diff*16).toString(16);
        re.push(pattern);
      } else {
        diff = max[1] - min[1];
        tmax = min[1]*1 + diff-1;
        pattern = min[0];
        switch (diff){
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
        min = '' + (min*1 + diff*10);
        re.push(pattern);
      }
    }
    pattern = '';

    //if digits are different, get to same
    if (min[2]!=max[2]){
      if (hex){
        pattern = min[0] + min[1];
        diff = parseInt(max[2],16) - parseInt(min[2],16);
        if (parseInt(min[2],16)>9){
          minV = 'h';
        } else {
          minV = 'd';
        }
        if (parseInt(max[2],16)>9){
          maxV = 'h';
        } else {
          maxV = 'd';
        }
        switch (minV + maxV){
        case "dd":
          pattern += '[' + min[2];
          if (diff>1) {
            pattern += '-';
          }
          pattern += max[2] + ']';
          break;
        case "dh":
          diff = 9 - min[2];
          //firs get up to 9
          switch (diff){
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
          switch (max[2]){
          case 'a':
            pattern += 'a]';
            break;
          case 'b':
            pattern += 'ab]';
            break;
          default:
            pattern += 'a-' + max[2] + ']';
            break;
          }

          break;
        case "hh":
          pattern += '[' + min[2];
          if (diff>1) {
            pattern += '-';
          }
          pattern += max[2] + ']';
          break;
        }
        diff = parseInt(max[2],16) - parseInt(min[2],16);
        min = '' + (parseInt(min,16) + diff).toString(16);
        re.push(pattern);
      } else {
        diff = max[2] - min[2];
        pattern = min[0] + min[1];
        switch (diff){
        case 0:
          pattern += min[2];
          break;
        case 1:
          pattern += '[' + min[2] + max[2] + ']';
          break;
        default:
         pattern += '[' + min[2] + '-' + max[2] + ']';
          break;
        }
        min = '' + (min*1 + diff);
        re.push(pattern);
      }
    }
    pattern = '';

    //last place is whole hundred
    if (min[2]=='0' && max[2]=='0') {
      pattern = max;
      re.push(pattern);
    }
    pattern = '';

    cnt = re.length;
    if (cnt==1){
      pattern = re[0];
    } else {
      pattern = re.join(')|(');
      pattern = '((' + pattern + '))';
    }
    return pattern;
  },
  regex: function (query,fuzz){
    query = sw10.query(query);
    if (!query) {
      return '';
    }
    var matches;
    var i;
    var fsw_pattern;
    var part;
    var from;
    var to;
    var re_range;
    var segment;
    var x;
    var y;
    var base;
    var fill;
    var rotate;
    if (!fuzz) {
      fuzz = 20;
    }
    var re_sym = 'S[123][0-9a-f]{2}[0-5][0-9a-f]';
    var re_coord = '[0-9]{3}x[0-9]{3}';
    var re_word = '[BLMR](' + re_coord + ')(' + re_sym + re_coord + ')*';
    var re_term = '(A(' + re_sym+ ')+)';
    var q_range = 'R[123][0-9a-f]{2}t[123][0-9a-f]{2}';
    var q_sym = 'S[123][0-9a-f]{2}[0-5u][0-9a-fu]';
    var q_coord = '([0-9]{3}x[0-9]{3})?';
    var q_var = '(V[0-9]+)';
    var q_term;
    query = sw10.query(query);
    if (!query) {return '';}
    if (query=='Q'){
      return [re_term + "?" + re_word];
    }
    if (query=='QT'){
      return [re_term + re_word];
    }
    var segments = [];
    var term = query.indexOf('T')+1;
    if (term){
      q_term = '(A';
      var qat = query.slice(0,term);
      query = query.replace(qat,'');
      if (qat == 'QT') {
        q_term += '(' + re_sym + ')+)';
      } else {
        matches = qat.match(new RegExp('(' + q_sym + '|' + q_range + ')','g'));
        if (matches){
          var matched;
          for(i=0; i<matches.length; i+=1) {
            matched = matches[i].match(new RegExp(q_sym));
            if (matched){
              segment = matched[0].slice(0,4);
              fill = matched[0].slice(4,5);
              if (fill=='u') {
                segment += '[0-5]';
              } else {
                segment += fill;
              }
              rotate = matched[0].slice(5,6);
              if (rotate=='u') {
                segment += '[0-9a-f]';
              } else {
                segment += rotate;
              }
              q_term += segment;
            } else {
              from = matches[i].slice(1,4);
              to = matches[i].slice(5,8);
              re_range = sw10.range(from,to,'hex');
              segment = 'S' + re_range + '[0-5][0-9a-f]';
              q_term += segment;
            }
          }
          q_term += '(' + re_sym + ')*)';
        }
      }
    }
    //get the variance
    matches = query.match(new RegExp(q_var,'g'));
    if (matches) {
      fuzz = matches.toString().slice(1)*1;
    }
    //this gets all symbols with or without location
    fsw_pattern = q_sym + q_coord;
    matches = query.match(new RegExp(fsw_pattern,'g'));
    if (matches){
      for(i=0; i<matches.length; i+=1) {
        part = matches[i].toString();
        base = part.slice(1,4);
        segment = 'S' + base;
        fill = part.slice(4,5);
        if (fill=='u') {
          segment += '[0-5]';
        } else {
          segment += fill;
        }
        rotate = part.slice(5,6);
        if (rotate=='u') {
          segment += '[0-9a-f]';
        } else {
          segment += rotate;
        }
        if (part.length>6){
          x = part.slice(6,9)*1;
          y = part.slice(10,13)*1;
          //now get the x segment range+++
          segment += sw10.range((x-fuzz),(x+fuzz));
          segment += 'x';
          segment += sw10.range((y-fuzz),(y+fuzz));
        } else {
          segment += re_coord;
        }
        //now I have the specific search symbol
        // add to general ksw word
        segment = re_word + segment + '(' + re_sym + re_coord + ')*';
        if (term) {
          segment = q_term + segment;
        } else {
          segment = re_term + "?" + segment;
        }
        segments.push(segment);
      }
    }
    //this gets all ranges
    fsw_pattern = q_range + q_coord;
    matches = query.match(new RegExp(fsw_pattern,'g'));
    if (matches){
      for(i=0; i<matches.length; i+=1) {
        part = matches[i].toString();
        from = part.slice(1,4);
        to = part.slice(5,8);
        re_range = sw10.range(from,to,"hex");
        segment = 'S' + re_range + '[0-5][0-9a-f]';
        if (part.length>8){
          x = part.slice(8,11)*1;
          y = part.slice(12,15)*1;
          //now get the x segment range+++
          segment += sw10.range((x-fuzz),(x+fuzz));
          segment += 'x';
          segment += sw10.range((y-fuzz),(y+fuzz));
        } else {
          segment += re_coord;
        }
        // add to general ksw word
        segment = re_word + segment + '(' + re_sym + re_coord + ')*';
        if (term) {
          segment = q_term + segment;
        } else {
          segment = re_term + "?" + segment;
        }
        segments.push(segment);
      }
    }
    if (!segments.length){
      segments.push(q_term + re_word);
    }
    return segments;
  },
  results: function (query,text,lane){
    if (!text) {return [];}
    if("BLMR".indexOf(lane) === -1 || lane.length>1) {
      lane='';
    }
    var pattern;
    var matches;
    var parts;
    var words;
    var re = sw10.regex(query);
    if (!re) {return [];}
    var i;
    for(i=0; i<re.length; i+=1) {
      pattern = re[i];
      matches = text.match(new RegExp(pattern,'g'));
      if (matches){
        text = matches.join(' ');
      } else {
        text ='';
      }
    }
    if (text){
      if (lane){
        text = text.replace(/B/g,lane);
        text = text.replace(/L/g,lane);
        text = text.replace(/M/g,lane);
        text = text.replace(/R/g,lane);
      }
      parts = text.split(' ');
      words = parts.filter(function(element) {
        return element in parts ? false : parts[element] = true;
      }, {});
    } else {
      words = [];
    }
    return words;
  },
  lines: function (query,text,lane){
    if (!text) {return [];}
    if("BLMR".indexOf(lane) === -1 || lane.length>1) {
      lane='';
    }
    var pattern;
    var matches;
    var parts;
    var words;
    var re = sw10.regex(query);
    if (!re) {return [];}
    var i;
    for(i=0; i<re.length; i+=1) {
      pattern = re[i];
      pattern = '^' + pattern + '.*';
      matches = text.match(new RegExp(pattern,'mg'));
      if (matches){
        text = matches.join("\n");
      } else {
        text ='';
      }
    }
    if (text){
      if (lane){
        text = text.replace(/B/g,lane);
        text = text.replace(/L/g,lane);
        text = text.replace(/M/g,lane);
        text = text.replace(/R/g,lane);
      }
      parts = text.split("\n");
      words = parts.filter(function(element) {
        return element in parts ? false : parts[element] = true;
      }, {});
    } else {
      words = [];
    }
    return words;
  },
  convert: function (fsw,flags){
    // update to new set of flags
    // A - exact symbol in temporal prefix
    // a - general symbol in temporal prefix
    // S - exact symbol in spatial signbox
    // s - general symbol in spatial signbox
    // L - spatial signbox symbol at location
    var i;
    var query = '';
    if (sw10.fsw(fsw)){
      if (/^[Aa]?([Ss]L?)?$/.test(flags)){
        var re_base = 'S[123][0-9a-f]{2}';
        var re_sym = re_base + '[0-5][0-9a-f]';
        var re_coord = '[0-9]{3}x[0-9]{3}';
        var matches;
        var matched;

        if (flags.indexOf('A') > -1 || flags.indexOf('a') > -1) {
          //exact symbols or general symbols in order
          matches = fsw.match(new RegExp('A(' + re_sym + ')*','g'));
          if (matches){
            matched = matches[0];
            if (flags.indexOf('A') > -1) {
              query += matched + "T";
            } else {
              matches = matched.match(new RegExp(re_base,'g'));
              query += "A";
              for(i=0; i<matches.length; i+=1) {
                query += matches[i] + "uu";
              }
              query += "T";
            }
          }
        }

        if (flags.indexOf('S') > -1 || flags.indexOf('s') > -1) {
          //exact symbols or general symbols in spatial
          matches = fsw.match(new RegExp(re_sym + re_coord,'g'));
          if (matches){
            for(i=0; i<matches.length; i+=1) {
              if (flags.indexOf('S') > -1) {
                query += matches[i].slice(0,6);
              } else {
                query += matches[i].slice(0,4) + "uu";
              }
              if (flags.indexOf('L') > -1) {
                query += matches[i].slice(6,13);
              }
            }
          }
        }
      }
    }
    return query?"Q" + query:'';
  },
  signtext: function (signtext){
    var pattern = sw10.regex('Q');
    pattern = pattern[0];
    pattern = '(' + pattern + '|S3[0-9a-f]{2}[0-5][0-9a-f]([0-9]{3}x[0-9]{3})' + ')';
    var matches = signtext.match(new RegExp(pattern,'mg'));
    return matches?matches:[];
//    var key = text.match(/S[123][0-9a-f]{2}[0-5][0-9a-f]([0-9]{3}x[0-9]{3})?/g);
  }
};
