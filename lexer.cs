/* This file was generated by SableCC (http://www.sablecc.org/). */

using System;
using System.Collections;
using System.Text;
using System.IO;
using CS426.node;

namespace CS426.lexer {

internal class PushbackReader {
  private TextReader reader;
  private Stack stack = new Stack ();


  internal PushbackReader (TextReader reader)
  {
    this.reader = reader;
  }

  internal int Peek ()
  {
    if ( stack.Count > 0 ) return (int)stack.Peek();
    return reader.Peek();
  }

  internal int Read ()
  {
    if ( stack.Count > 0 ) return (int)stack.Pop();
    return reader.Read();
  }

  internal void Unread (int v)
  {
    stack.Push (v);
  }
}

public class LexerException : ApplicationException
{
    public LexerException(String message) : base (message)
    {
    }
}

public class Lexer
{
    protected Token token;
    protected State currentState = State.INITIAL;

    private PushbackReader input;
    private int line;
    private int pos;
    private bool cr;
    private bool eof;
    private StringBuilder text = new StringBuilder();

    protected virtual void Filter()
    {
    }

    public Lexer(TextReader input)
    {
        this.input = new PushbackReader(input);
    }

    public virtual Token Peek()
    {
        while(token == null)
        {
            token = GetToken();
            Filter();
        }

        return token;
    }

    public virtual Token Next()
    {
        while(token == null)
        {
            token = GetToken();
            Filter();
        }

        Token result = token;
        token = null;
        return result;
    }

    protected virtual Token GetToken()
    {
        int dfa_state = 0;

        int start_pos = pos;
        int start_line = line;

        int accept_state = -1;
        int accept_token = -1;
        int accept_length = -1;
        int accept_pos = -1;
        int accept_line = -1;

        int[][][] gotoTable = Lexer.gotoTable[currentState.id()];
        int[] accept = Lexer.accept[currentState.id()];
        text.Length = 0;

        while(true)
        {
            int c = GetChar();

            if(c != -1)
            {
                switch(c)
                {
                case 10:
                    if(cr)
                    {
                        cr = false;
                    }
                    else
                    {
                        line++;
                        pos = 0;
                    }
                    break;
                case 13:
                    line++;
                    pos = 0;
                    cr = true;
                    break;
                default:
                    pos++;
                    cr = false;
                    break;
                };

                text.Append((char) c);
                do
                {
                    int oldState = (dfa_state < -1) ? (-2 -dfa_state) : dfa_state;

                    dfa_state = -1;

                    int[][] tmp1 =  gotoTable[oldState];
                    int low = 0;
                    int high = tmp1.Length - 1;

                    while(low <= high)
                    {
                        int middle = (low + high) / 2;
                        int[] tmp2 = tmp1[middle];

                        if(c < tmp2[0])
                        {
                            high = middle - 1;
                        }
                        else if(c > tmp2[1])
                        {
                            low = middle + 1;
                        }
                        else
                        {
                            dfa_state = tmp2[2];
                            break;
                        }
                    }
                }while(dfa_state < -1);
            }
            else
            {
                dfa_state = -1;
            }

            if(dfa_state >= 0)
            {
                if(accept[dfa_state] != -1)
                {
                    accept_state = dfa_state;
                    accept_token = accept[dfa_state];
                    accept_length = text.Length;
                    accept_pos = pos;
                    accept_line = line;
                }
            }
            else
            {
                if(accept_state != -1)
                {
                    switch(accept_token)
                    {
                    case 0:
                        {
                            Token token = New0(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 1:
                        {
                            Token token = New1(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 2:
                        {
                            Token token = New2(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 3:
                        {
                            Token token = New3(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 4:
                        {
                            Token token = New4(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 5:
                        {
                            Token token = New5(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 6:
                        {
                            Token token = New6(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 7:
                        {
                            Token token = New7(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 8:
                        {
                            Token token = New8(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 9:
                        {
                            Token token = New9(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 10:
                        {
                            Token token = New10(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 11:
                        {
                            Token token = New11(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 12:
                        {
                            Token token = New12(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 13:
                        {
                            Token token = New13(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 14:
                        {
                            Token token = New14(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 15:
                        {
                            Token token = New15(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 16:
                        {
                            Token token = New16(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 17:
                        {
                            Token token = New17(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 18:
                        {
                            Token token = New18(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 19:
                        {
                            Token token = New19(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 20:
                        {
                            Token token = New20(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 21:
                        {
                            Token token = New21(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 22:
                        {
                            Token token = New22(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 23:
                        {
                            Token token = New23(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 24:
                        {
                            Token token = New24(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 25:
                        {
                            Token token = New25(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 26:
                        {
                            Token token = New26(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 27:
                        {
                            Token token = New27(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 28:
                        {
                            Token token = New28(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 29:
                        {
                            Token token = New29(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 30:
                        {
                            Token token = New30(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 31:
                        {
                            Token token = New31(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 32:
                        {
                            Token token = New32(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    case 33:
                        {
                            Token token = New33(
                                GetText(accept_length),
                                start_line + 1,
                                start_pos + 1);
                            PushBack(accept_length);
                            pos = accept_pos;
                            line = accept_line;
                            return token;
                        }
                    }
                }
                else
                {
                    if(text.Length > 0)
                    {
                        throw new LexerException(
                            "[" + (start_line + 1) + "," + (start_pos + 1) + "]" +
                            " Unknown token: " + text);
                    }
                    else
                    {
                        EOF token = new EOF(
                            start_line + 1,
                            start_pos + 1);
                        return token;
                    }
                }
            }
        }
    }

    private Token New0(String text, int line, int pos) { return new TAssign(text, line, pos); }
    private Token New1(String text, int line, int pos) { return new TPlus(text, line, pos); }
    private Token New2(String text, int line, int pos) { return new TMinus(text, line, pos); }
    private Token New3(String text, int line, int pos) { return new TMult(text, line, pos); }
    private Token New4(String text, int line, int pos) { return new TDiv(text, line, pos); }
    private Token New5(String text, int line, int pos) { return new TEol(text, line, pos); }
    private Token New6(String text, int line, int pos) { return new TComma(text, line, pos); }
    private Token New7(String text, int line, int pos) { return new TPromise(text, line, pos); }
    private Token New8(String text, int line, int pos) { return new TLeftParenthesis(text, line, pos); }
    private Token New9(String text, int line, int pos) { return new TRightParenthesis(text, line, pos); }
    private Token New10(String text, int line, int pos) { return new TOpenBrace(text, line, pos); }
    private Token New11(String text, int line, int pos) { return new TCloseBrace(text, line, pos); }
    private Token New12(String text, int line, int pos) { return new TAnd(text, line, pos); }
    private Token New13(String text, int line, int pos) { return new TOr(text, line, pos); }
    private Token New14(String text, int line, int pos) { return new TNot(text, line, pos); }
    private Token New15(String text, int line, int pos) { return new TLt(text, line, pos); }
    private Token New16(String text, int line, int pos) { return new TGt(text, line, pos); }
    private Token New17(String text, int line, int pos) { return new TLte(text, line, pos); }
    private Token New18(String text, int line, int pos) { return new TGte(text, line, pos); }
    private Token New19(String text, int line, int pos) { return new TEq(text, line, pos); }
    private Token New20(String text, int line, int pos) { return new TNeq(text, line, pos); }
    private Token New21(String text, int line, int pos) { return new TWhile(text, line, pos); }
    private Token New22(String text, int line, int pos) { return new TIf(text, line, pos); }
    private Token New23(String text, int line, int pos) { return new TElif(text, line, pos); }
    private Token New24(String text, int line, int pos) { return new TElse(text, line, pos); }
    private Token New25(String text, int line, int pos) { return new TReturn(text, line, pos); }
    private Token New26(String text, int line, int pos) { return new TMain(text, line, pos); }
    private Token New27(String text, int line, int pos) { return new TConstant(text, line, pos); }
    private Token New28(String text, int line, int pos) { return new TId(text, line, pos); }
    private Token New29(String text, int line, int pos) { return new TComment(text, line, pos); }
    private Token New30(String text, int line, int pos) { return new TDouble(text, line, pos); }
    private Token New31(String text, int line, int pos) { return new TInteger(text, line, pos); }
    private Token New32(String text, int line, int pos) { return new TString(text, line, pos); }
    private Token New33(String text, int line, int pos) { return new TBlank(text, line, pos); }

    private int GetChar()
    {
        if(eof)
        {
            return -1;
        }

        int result = input.Read();

        if(result == -1)
        {
            eof = true;
        }

        return result;
    }

    private void PushBack(int acceptLength)
    {
        int length = text.Length;
        for(int i = length - 1; i >= acceptLength; i--)
        {
            eof = false;

            input.Unread(text[i]);
        }
    }


    protected virtual void Unread(Token token)
    {
        String text = token.Text;
        int length = text.Length;

        for(int i = length - 1; i >= 0; i--)
        {
            eof = false;

            input.Unread(text[i]);
        }

        pos = token.Pos - 1;
        line = token.Line - 1;
    }

    private string GetText(int acceptLength)
    {
        StringBuilder s = new StringBuilder(acceptLength);
        for(int i = 0; i < acceptLength; i++)
        {
            s.Append(text[i]);
        }

        return s.ToString();
    }

    private static int[][][][] gotoTable = {
      new int[][][] {
        new int[][] {
          new int[] {9, 9, 1},
          new int[] {10, 10, 2},
          new int[] {13, 13, 3},
          new int[] {32, 32, 4},
          new int[] {33, 33, 5},
          new int[] {34, 34, 6},
          new int[] {40, 40, 7},
          new int[] {41, 41, 8},
          new int[] {42, 42, 9},
          new int[] {43, 43, 10},
          new int[] {44, 44, 11},
          new int[] {45, 45, 12},
          new int[] {47, 47, 13},
          new int[] {48, 57, 14},
          new int[] {58, 58, 15},
          new int[] {59, 59, 16},
          new int[] {60, 60, 17},
          new int[] {61, 61, 18},
          new int[] {62, 62, 19},
          new int[] {65, 90, 20},
          new int[] {95, 95, 21},
          new int[] {97, 97, 22},
          new int[] {98, 98, 20},
          new int[] {99, 99, 23},
          new int[] {100, 100, 20},
          new int[] {101, 101, 24},
          new int[] {102, 104, 20},
          new int[] {105, 105, 25},
          new int[] {106, 108, 20},
          new int[] {109, 109, 26},
          new int[] {110, 110, 27},
          new int[] {111, 111, 28},
          new int[] {112, 113, 20},
          new int[] {114, 114, 29},
          new int[] {115, 118, 20},
          new int[] {119, 119, 30},
          new int[] {120, 122, 20},
          new int[] {123, 123, 31},
          new int[] {125, 125, 32},
        },
        new int[][] {
          new int[] {9, 32, -2},
        },
        new int[][] {
          new int[] {9, 32, -2},
        },
        new int[][] {
          new int[] {9, 32, -2},
        },
        new int[][] {
          new int[] {9, 32, -2},
        },
        new int[][] {
          new int[] {61, 61, 33},
        },
        new int[][] {
          new int[] {32, 33, 34},
          new int[] {34, 34, 35},
          new int[] {35, 91, 34},
          new int[] {92, 92, 36},
          new int[] {93, 126, 34},
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
          new int[] {47, 47, 37},
        },
        new int[][] {
          new int[] {46, 46, 38},
          new int[] {48, 57, 14},
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
          new int[] {61, 61, 39},
        },
        new int[][] {
          new int[] {61, 61, 40},
        },
        new int[][] {
          new int[] {61, 61, 41},
        },
        new int[][] {
          new int[] {48, 57, 42},
          new int[] {65, 90, 43},
          new int[] {95, 95, 21},
          new int[] {97, 122, 43},
        },
        new int[][] {
          new int[] {65, 90, 44},
          new int[] {97, 122, 44},
        },
        new int[][] {
          new int[] {48, 95, -22},
          new int[] {97, 109, 43},
          new int[] {110, 110, 45},
          new int[] {111, 122, 43},
        },
        new int[][] {
          new int[] {48, 95, -22},
          new int[] {97, 110, 43},
          new int[] {111, 111, 46},
          new int[] {112, 122, 43},
        },
        new int[][] {
          new int[] {48, 95, -22},
          new int[] {97, 107, 43},
          new int[] {108, 108, 47},
          new int[] {109, 122, 43},
        },
        new int[][] {
          new int[] {48, 95, -22},
          new int[] {97, 101, 43},
          new int[] {102, 102, 48},
          new int[] {103, 122, 43},
        },
        new int[][] {
          new int[] {48, 95, -22},
          new int[] {97, 97, 49},
          new int[] {98, 122, 43},
        },
        new int[][] {
          new int[] {48, 110, -25},
          new int[] {111, 111, 50},
          new int[] {112, 122, 43},
        },
        new int[][] {
          new int[] {48, 95, -22},
          new int[] {97, 113, 43},
          new int[] {114, 114, 51},
          new int[] {115, 122, 43},
        },
        new int[][] {
          new int[] {48, 95, -22},
          new int[] {97, 100, 43},
          new int[] {101, 101, 52},
          new int[] {102, 122, 43},
        },
        new int[][] {
          new int[] {48, 95, -22},
          new int[] {97, 103, 43},
          new int[] {104, 104, 53},
          new int[] {105, 122, 43},
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
          new int[] {32, 126, -8},
        },
        new int[][] {
        },
        new int[][] {
          new int[] {32, 33, 34},
          new int[] {34, 34, 54},
          new int[] {35, 126, -8},
        },
        new int[][] {
          new int[] {0, 9, 55},
          new int[] {11, 12, 55},
          new int[] {14, 65535, 55},
        },
        new int[][] {
          new int[] {48, 57, 56},
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
        },
        new int[][] {
          new int[] {48, 57, 42},
          new int[] {65, 90, 57},
          new int[] {97, 122, 57},
        },
        new int[][] {
          new int[] {48, 122, -22},
        },
        new int[][] {
          new int[] {48, 122, -44},
        },
        new int[][] {
          new int[] {48, 95, -22},
          new int[] {97, 99, 43},
          new int[] {100, 100, 58},
          new int[] {101, 122, 43},
        },
        new int[][] {
          new int[] {48, 109, -24},
          new int[] {110, 110, 59},
          new int[] {111, 122, 43},
        },
        new int[][] {
          new int[] {48, 95, -22},
          new int[] {97, 104, 43},
          new int[] {105, 105, 60},
          new int[] {106, 114, 43},
          new int[] {115, 115, 61},
          new int[] {116, 122, 43},
        },
        new int[][] {
          new int[] {48, 122, -22},
        },
        new int[][] {
          new int[] {48, 104, -49},
          new int[] {105, 105, 62},
          new int[] {106, 122, 43},
        },
        new int[][] {
          new int[] {48, 95, -22},
          new int[] {97, 115, 43},
          new int[] {116, 116, 63},
          new int[] {117, 122, 43},
        },
        new int[][] {
          new int[] {48, 122, -22},
        },
        new int[][] {
          new int[] {48, 115, -52},
          new int[] {116, 116, 64},
          new int[] {117, 122, 43},
        },
        new int[][] {
          new int[] {48, 104, -49},
          new int[] {105, 105, 65},
          new int[] {106, 122, 43},
        },
        new int[][] {
          new int[] {32, 126, -8},
        },
        new int[][] {
          new int[] {0, 65535, -39},
        },
        new int[][] {
          new int[] {48, 57, 56},
          new int[] {69, 69, 66},
          new int[] {101, 101, 67},
        },
        new int[][] {
          new int[] {48, 122, -44},
        },
        new int[][] {
          new int[] {48, 122, -22},
        },
        new int[][] {
          new int[] {48, 95, -22},
          new int[] {97, 114, 43},
          new int[] {115, 115, 68},
          new int[] {116, 122, 43},
        },
        new int[][] {
          new int[] {48, 101, -27},
          new int[] {102, 102, 69},
          new int[] {103, 122, 43},
        },
        new int[][] {
          new int[] {48, 100, -31},
          new int[] {101, 101, 70},
          new int[] {102, 122, 43},
        },
        new int[][] {
          new int[] {48, 109, -24},
          new int[] {110, 110, 71},
          new int[] {111, 122, 43},
        },
        new int[][] {
          new int[] {48, 122, -22},
        },
        new int[][] {
          new int[] {48, 95, -22},
          new int[] {97, 116, 43},
          new int[] {117, 117, 72},
          new int[] {118, 122, 43},
        },
        new int[][] {
          new int[] {48, 107, -26},
          new int[] {108, 108, 73},
          new int[] {109, 122, 43},
        },
        new int[][] {
          new int[] {45, 45, 74},
          new int[] {48, 57, 75},
        },
        new int[][] {
          new int[] {45, 57, -68},
        },
        new int[][] {
          new int[] {48, 115, -52},
          new int[] {116, 116, 76},
          new int[] {117, 122, 43},
        },
        new int[][] {
          new int[] {48, 122, -22},
        },
        new int[][] {
          new int[] {48, 122, -22},
        },
        new int[][] {
          new int[] {48, 122, -22},
        },
        new int[][] {
          new int[] {48, 113, -30},
          new int[] {114, 114, 77},
          new int[] {115, 122, 43},
        },
        new int[][] {
          new int[] {48, 100, -31},
          new int[] {101, 101, 78},
          new int[] {102, 122, 43},
        },
        new int[][] {
          new int[] {48, 57, 75},
        },
        new int[][] {
          new int[] {48, 57, 75},
        },
        new int[][] {
          new int[] {48, 122, -22},
        },
        new int[][] {
          new int[] {48, 109, -24},
          new int[] {110, 110, 79},
          new int[] {111, 122, 43},
        },
        new int[][] {
          new int[] {48, 122, -22},
        },
        new int[][] {
          new int[] {48, 122, -22},
        },
      },
    };

    private static int[][] accept = {
      new int[] {
        -1, 33, 33, 33, 33, -1, -1, 8, 9, 3, 1, 6, 2, 4, 31, 7, 
        5, 15, 0, 16, 28, -1, 28, 28, 28, 28, 28, 28, 28, 28, 28, 10, 
        11, 20, -1, 32, -1, 29, -1, 17, 19, 18, 28, 28, 28, 28, 28, 28, 
        22, 28, 28, 13, 28, 28, 32, 29, 30, 28, 12, 28, 28, 28, 28, 14, 
        28, 28, -1, -1, 28, 23, 24, 26, 28, 28, -1, 30, 27, 28, 21, 25, 
        
      },
    };

    public class State
    {
        public static State INITIAL = new State(0);

        private int _id;

        private State(int _id)
        {
            this._id = _id;
        }

        public int id()
        {
            return _id;
        }
    }
}
}
