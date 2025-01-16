Imports System
Imports System.Runtime.CompilerServices

Module Program
    Sub Main(args As String())
        Dim k As Integer, position As Integer, new_bid As Integer
        Dim str_hand As String, entered_hand As String, meaning As String
        Dim suits()() As String
        Dim arr_leads() As String
        Dim arr_leaders() As Integer
        Dim arr_suits() As String
        Dim info() As Integer
        ReDim hand(3)
        ReDim suits(3)
        ReDim arr_bids(63)
        ReDim arr_leads(63)
        ReDim arr_leaders(51)

        For k = 0 To 3
            ReDim hand(k).suit(3)
        Next
        For k = 0 To 3
            Player(k) = New EPBot86.EPBot
            'Player(k) = New EPBot64.EPBot
            'Player(k) = New EPBotARM64.EPBot
        Next k

        set_board()
        set_dealers()
        set_vulnerability()
        set_strain_mark()

        '---example hand
        str_hand = "01BB8519F369D29DF04BA17EF45D"
        Console.WriteLine("Current hand: " & str_hand)
        Console.WriteLine("Enter another deal or skip")
        entered_hand = Console.ReadLine()
        If Len(entered_hand) = 28 Then
            str_hand = entered_hand
        End If
        set_hand(str_hand)
        Console.WriteLine("Entered hand: " & str_hand)
        Console.WriteLine("")

        For i = C_SPADES To C_CLUBS Step -1
            Console.WriteLine(vbTab + hand(0).suit(i))
        Next i
        Console.WriteLine("")
        For i = C_SPADES To C_CLUBS Step -1
            Console.WriteLine(hand(3).suit(i) + vbTab + vbTab + hand(1).suit(i))
        Next i
        Console.WriteLine("")
        For i = C_SPADES To C_CLUBS Step -1
            Console.WriteLine(vbTab + hand(2).suit(i))
        Next i

        Console.WriteLine("")
        Console.WriteLine("Deal = " & CStr(deal))
        Console.WriteLine("Dealer = " & CStr(dealer))
        Console.WriteLine("Vulnerable = " & CStr(vulnerable))
        Console.WriteLine("")
        ''hands
        For k = 0 To 3
            Player(k).new_hand(k, hand(k).suit, dealer, vulnerable)
            Player(k).scoring = 0
            ''examples for team 1 NS
            Player(k).system_type(0) = T_21GF
            Player(k).conventions(0, "Cue bid") = 1
            ''examples for team 2 WE
            Player(k).system_type(1) = T_21GF
            Player(k).conventions(1, "Cue bid") = 1
        Next k

        bid()

        Console.WriteLine("W" + vbTab + "N" + vbTab + "E" + vbTab + "S" + vbTab)
        Console.WriteLine(bidding_body)
        Console.WriteLine("")
        Console.WriteLine("Declarer = " & CStr(declarer))
        Console.WriteLine("Leader = " & CStr(leader))
        Console.WriteLine("Dummy = " & CStr(dummy))
        Console.WriteLine("")

        For k = 0 To 3
            With Player(k)
                Console.WriteLine("")
                Console.WriteLine("Player cards visible from position: " & CStr(k))
                'set hand
                .new_hand(k, hand(k).suit, dealer, vulnerable)
                ''set the entire auction
                .set_arr_bids(arr_bids)
                ''optional set dummy (for play)
                .set_dummy(dummy, hand(dummy).suit)
                ''optional set previous leads (for play)
                arr_leaders(0) = leader
                ''arr_leads(0) = "TS" (for play)
                .set_arr_leads(arr_leaders, arr_leads)
                ''obtain a bid based on the previous auction
                new_bid = .get_bid
                ''obtain all hands
                arr_suits = .get_arr_suits()
                Console.WriteLine("")
                For i = C_SPADES To C_CLUBS Step -1
                    Console.WriteLine(vbTab + arr_suits(i))
                Next i
                Console.WriteLine("")
                For i = C_SPADES To C_CLUBS Step -1
                    Console.WriteLine(arr_suits(12 + i) + vbTab + vbTab + arr_suits(4 + i))
                Next i
                Console.WriteLine("")
                For i = C_SPADES To C_CLUBS Step -1
                    Console.WriteLine(vbTab + arr_suits(8 + i))
                Next i
                Console.WriteLine("")
            End With
        Next k

        Console.ReadKey()
    End Sub
End Module
