using System;
using System.Collections.Generic;
using System.Linq;

namespace DomConsult.GlobalShared.Utilities
{
    /// <summary>
    /// Enum TFormState
    /// </summary>
    public enum TFormState
    {
        /// <summary>
        /// The CFS none
        /// </summary>
        cfsNone = 0x0,

        /// <summary>
        /// The CFS changes forbidden
        /// </summary>
        cfsChangesForbidden = 0x1,

        /// <summary>
        /// The CFS no changes
        /// </summary>
        cfsNoChanges = 0x8,

        /// <summary>
        /// The CFS view
        /// </summary>
        cfsView = 0x10,

        /// <summary>
        /// The CFS edit
        /// </summary>
        cfsEdit = 0x20,
        /// <summary>
        /// The CFS new
        /// </summary>
        cfsNew = 0x40,

        /// <summary>
        /// The CFS update
        /// </summary>
        cfsUpdate = 0x80,

        /// <summary>
        /// The CFS cancel
        /// </summary>
        cfsCancel = 0x100,

        /// <summary>
        /// The CFS delete
        /// </summary>
        cfsDelete = 0x200,

        /// <summary>
        /// The CFS close bd
        /// </summary>
        cfsCloseBD = 0x400,

        /// <summary>
        /// The CFS ask again
        /// </summary>
        cfsAskAgain = 0x800,

        /// <summary>
        /// The CFS button pressed
        /// </summary>
        cfsButtonPressed = 0x1000
    }

    /// <summary>
    /// Enum TBDFieldProps
    /// </summary>
    public enum TBDFieldProps
    {
        /// <summary>
        /// The c cell hint
        /// </summary>
        cCellHint,

        /// <summary>
        /// The c cell read only
        /// </summary>
        cCellReadOnly,

        /// <summary>
        /// The c cell value
        /// </summary>
        cCellValue,

        /// <summary>
        /// The c color editable
        /// </summary>
        cColorEditable,

        /// <summary>
        /// The c color read only
        /// </summary>
        cColorReadOnly,

        /// <summary>
        /// The c columns information
        /// </summary>
        cColumnsInfo,

        /// <summary>
        /// The c data type
        /// </summary>
        cDataType,

        /// <summary>
        /// The c dummy
        /// </summary>
        cDummy,

        /// <summary>
        /// The c dummy2
        /// </summary>
        cDummy2,

        /// <summary>
        /// The c dummy3
        /// </summary>
        cDummy3,

        /// <summary>
        /// The c dummy4
        /// </summary>
        cDummy4,

        /// <summary>
        /// The c dummy5
        /// </summary>
        cDummy5,

        /// <summary>
        /// The c enabled
        /// </summary>
        cEnabled,

        /// <summary>
        /// The c font color
        /// </summary>
        cFontColor,

        /// <summary>
        /// The c font name
        /// </summary>
        cFontName,

        /// <summary>
        /// The c font size
        /// </summary>
        cFontSize,

        /// <summary>
        /// The c font style
        /// </summary>
        cFontStyle,

        /// <summary>
        /// The c grid column
        /// </summary>
        cGridColumn,

        /// <summary>
        /// The c grid row
        /// </summary>
        cGridRow,

        /// <summary>
        /// The c hint
        /// </summary>
        cHint,

        /// <summary>
        /// The c hint map
        /// </summary>
        cHintMap,

        /// <summary>
        /// The c input status
        /// </summary>
        cInputStatus,

        /// <summary>
        /// The c maximum length
        /// </summary>
        cMaxLength,

        /// <summary>
        /// The c modify status
        /// </summary>
        cModifyStatus,

        /// <summary>
        /// The c multi selection
        /// </summary>
        cMultiSelection,

        /// <summary>
        /// The c object type identifier
        /// </summary>
        cObjectTypeId,

        /// <summary>
        /// The c password character
        /// </summary>
        cPasswordChar,

        /// <summary>
        /// The c read only map
        /// </summary>
        cReadOnlyMap,

        /// <summary>
        /// The c SQL
        /// </summary>
        cSQL,

        /// <summary>
        /// The c SQL sort by
        /// </summary>
        cSQLSortBy,

        /// <summary>
        /// The c tool identifier
        /// </summary>
        cToolId,

        /// <summary>
        /// The c visible
        /// </summary>
        cVisible
    }

    /// <summary>
    /// Enum TBDOthers
    /// </summary>
    public enum TBDOthers
    {
        /// <summary>
        /// Rozdzielczoœæ ekranu na którym jest uruchomiona aplikacja, np.: 1920x1080
        /// </summary>
        coScreenSize = -14, // BD--+Com!!!

        /// <summary>
        /// Nazwa exe-ka z jakiego zosta³ uruchomiony systemm, np.: granitxp; xeidox; granos
        /// </summary>
        coAppName = -13, // BD--+Com!!!

        /// <summary>
        /// (+--) DesignTime – je¿eli jest i ma wartosc != 0 to oznacza, ¿e aplikacja pracuje w trybie DesignTime
        /// </summary>
        coDesignTime = -5, // BD--+Com!!!

        /// <summary>
        /// ObjectId
        /// </summary>
        coObjectId = 1, // Com--+BD !!!

        /// <summary>
        /// ObjectTypeId
        /// </summary>
        coObjectTypeId = 2, // Com--+BD !!!

        /// <summary>
        /// (--+) DisabledFunction – jeœli nie ma, to domyœlnie przyjmowana jest wartoœæ 0 – brak blokowania funkcji formatki
        /// </summary>
        coDisabledFunction = 3, // Com--+BD !!!

        /// <summary>
        /// (--+) FormId – je¿eli zostanie zwrócona ta wartoœæ(i bêdzie ró¿na od FormId obecnie pokazywanej
        /// w BaseDetail’u), wówczas przed przypisaniem wartoœci do kontrolek, zostanie przebudowany formularz
        /// na BaseDetail’u(nowoœæ!!!!!).
        /// </summary>
        coFormId = 4, // Com--+BD !!!

        /// <summary>
        /// (--+) przekazanie do aplikacji nag³ówka formularza
        /// </summary>
        coFormCaption = 5, // Com--+BD !!!

        /// <summary>
        /// (--+) ForceReturnAllFieldName – wymusza zwracanie w tablicy Fields(dla zapisu, wycofania zmian oraz
        ///      aktualizacji kontrolek) wartoœci wszystkich kontrolek, a nie tylko tych które s¹ modyfikowalne(domyœlnie)
        /// </summary>
        coForceReturnAllFieldName = 6, // Com--+BD !!!

        /// <summary>
        ///(--+) RequiredFieldsArray – wymusza przekazania w parametrze Fields do metody GetSetValues podczas pokazywania
        /// rekordu do podgl¹du zamiast wartoœci Unassigned(domyœlnie), tablicy z wszystkimi FieldName’ami znalezionymi
        ///na formatce(aby to zadzia³a³o, w parametrze OTHERS z metody AssignStartUpParams musi byæ przekazane
        ///FormId dla formularza).
        /// </summary>
        coRequiredFieldsArray = 7, // Com--+BD !!!

        /// <summary>
        /// (+--) ActiveControlFieldName – FieldName kontrolki, na której wystapi³o zdarzen ie OnExit lub OnEnter lub
        /// FieldName dla którego jest wykonywane zdarzenie po wybraniu elementu na liscie LookUp
        /// </summary>
        coActiveControlFieldName = 8, // BD--+Com

        /// <summary>
        /// (+--) ActualizationType – typ zdarzenia które wystapi³o na kontrolce(OnExit= 1, OnEnter= 2)
        /// </summary>
        coActualizationType = 9, // BD--+Com

        /// <summary>
        /// (+--) ConfirmationResult – zwraca informacje o wyborze u¿ytkownika, który dokona³ on po pokazaniu mu
        /// komunikatu wymagajacego informacji zwrotnej do komponentu(Yes = 1, No = 2).
        /// </summary>
        coConfirmationResult = 10, // BD--+Com

        /// <summary>
        /// (--+) ReturnFieldNameDuringCancel – informacja, ¿e komponent ¿yczy sobie aby podczas Cancelu, otrzymywa³
        /// w parametrze Fields wartoœci zmiennych wprowadzone przez u¿ytkownika
        /// </summary>
        coReturnFieldNameDuringCancel = 11, // Com--+BD !!!

        /// <summary>
        /// (--+) CallAgain – informacja, ¿e komponent ¿yczy sobie aby jeszcze raz wywo³ano metodê SetGetValue
        /// </summary>
        coCallAgain = 12, // Com--+BD !!!

        /// <summary>
        /// (+--) RefreshAllDictionaries – informacja dla komponentu, ¿e maj¹ zostaæ odœwie¿one wszystkie skorowidze w tablicy Fields
        /// </summary>
        coRefreshAllDictionaries = 13, // BD--+Com

        /// <summary>
        /// (+--) ActiveControlValue – Jest to wartoœæ klucza g³ównego zaznaczonego na liœcie typu LookUp. W przypadku gdy
        /// na DDButton'ie ustawiony jest property MultipleExecute, wówczas na liœcie LookUp'a mo¿liwa jest
        /// multiselekcja i w tym parametrze zwracana jest zawsze wektor(tablica 1 - wymiarowa) z identyfikatorami
        /// wybranymi na liœcie.
        /// </summary>
        coActiveControlValue = 14, // BD--+Com

        /// <summary>
        /// (--+) RebuildForm – Ustawienie tego parametru powoduje wymuszenie przebudowania formularza - ma to
        /// zastosowanie przy DDPanelach, mo¿liwe jest wymuszenie w FormState = 8 ponownego odpytania o liczbê DDPaneli
        /// </summary>
        coRebuildForm = 15, // Com--+BD

        /// <summary>
        /// (--+) AddAllFieldValuesToArray – Ustawienie tego parametry powoduje wymuszenie wystawiania komponentowi
        /// wype³nionej tablicy Fields(zawartoœci¹ wszystkich kontrolek) w wo³aniu metody SetGetValue z FormState 8
        /// (bez zmian) lub 4096(nacisniecie DDButton'a). Przekazanie tej wartoœci w jednym OTHERS obowi¹zuje tak
        /// dlugo, a¿ nie zostanie odwo³ane(je¿eli w OTHERS nie pojawia sie dany typ wartoœci oznacza to ¿e siê ona
        /// nie zmienia - przyp.aut.)
        /// </summary>
        coAddAllFieldValuesToArray = 16, // Com--+BD

        /// <summary>
        /// (--+) AddEditabledFieldValuesToArray – Efekt analogiczy jak dla AddAllFieldValueToArray, z t¹ ró¿nic¹ ¿e do
        /// Fields trafiaj¹ tylko zawartoœci pó³ edytowalnych
        /// </summary>
        coAddEditableFieldValuesToArray = 17, // Com--+BD

        /// <summary>
        /// (--+) AllowProcessing – Mo¿liwe jest zablokowanie(brak wpisu w OTHERS lub wartosc 0) lub zezwolenie na
        /// uruchomienie(wartosc 1) akcji zwi¹zanej z naciskanym przyciskiem.
        /// </summary>
        coAllowProcessing = 18, // Com--+BD

        /// <summary>
        /// (+--) CancelCallAgain – Informacja, ¿e u¿ytkownik przerwa³ wykonywanie cyklu CallAgain(wartoœæ nie ma znaczenia).
        /// </summary>
        coCancelCallAgain = 19, // BD--+Com

        /// <summary>
        /// (+--) ModificationsCancelled – Informacja, ¿e Update/ Cancel / Delete zosta³ przerwany wewn¹trz komponentu -
        /// oznacza to ¿e BDC nie bêdzie wo³a³ ponownie SetGetValue z odpowiednimi flagami w³aœciwymi przerwanej
        /// operacji(oczywiœcie parametry wynikowe zostan¹ zanalizowane).
        /// </summary>
        coModificationsCancelled = 20, // BD--+Com

        /// <summary>
        /// (--+) IgnoreDDButtonsForFIELDSArray – Informacja, ¿e komponent nie chce(wartoœæ TRUE) otrzymywaæ w tablicy
        /// FIELDS wpisów dotycz¹cych kontrolek DDButton
        /// </summary>
        coIgnoreDDButtonsForFIELDSArray = 21, // Com--+BD

        /// <summary>
        /// (--+) EnterLikeTab – Wymuszenie, aby naciœniêcie klawisza ENTER w kontrolce formularza powodowa³o przejœcie
        /// do nstêpnej kontrolki(podobne zachowania jak naciœniêcie klawisza TAB)
        /// </summary>
        coEnterLikeTab = 22, // Com--+BD

        /// <summary>
        /// (--+) FieldNameToActivate – Przekazanie nazwy kontrolki(FieldName), która ma staæ siê kontrolk¹ aktywn¹ }
        /// </summary>
        coFieldNameToActivate = 23, // Com--+BD

        /// <summary>
        /// (+--/--+) ClientVariable – Ten OTHER omo¿liwia przechowanie u klienta dowolnej wartoœci typu OleVariant.
        /// Wartoœæ ta bêdzie dok³adana do ka¿dego OTHER'a podczas wo³ania metody SetGetValue tak d³ugo jak d³ugo bêdzie
        /// ró¿na od Unassigned.Ten OTHER ma szczególne wykorzystanie podczas pracy z kilkoma zaznaczonymi rekordami,
        /// gdy nale¿y przekazywaæ coœ pomiêdzy kolejnymi instancjami...
        /// </summary>
        coClientVariable = 24, // Com--+BD | BD--+Com

        /// <summary>
        /// (--+) ClearAllFields – Wymuszenie wyczyszczenie wszystkich kontrolek na formularzu
        /// </summary>
        coClearAllFields = 25, // Com--+BD

        /// <summary>
        /// (--+) InParams – parametr pozwalajacy zwrócic ci¹g parametrów wejsciowych dla BDC odpowiadaj¹cy bie¿¹cemu
        /// stanowi formularza -ma to zastosowanie je¿eli po utworzeniu nowego rekordu, chcemy aby rekord ten dopisal
        /// siê do listy i by mo¿na do niego wróciæ...
        /// </summary>
        coInParams = 26, // Com--+BD

        /// <summary>
        /// (--+) ForceRefresh – zwrócenie tego OTHER'a umo¿liwia wys³anie do rodzica (czyli do aplikacji, z której
        /// zosta³ formularz uruchomiony) informacji, ¿e ma siê on odœwie¿yæ.
        /// Pocz¹wszy od wersji 1399 tego othera mo¿na u¿yæ równie¿ w narzêdziu typu 7 aby wymusiæ odœwie¿enie.
        /// </summary>
        coForceRefresh = 27, // Com--+BD

        /// <summary>
        /// (--+) FormDefinition – poprzez tego OTHER'a mo¿na zwróciæ do BDC definicje formularza (jako ci¹g znaków)
        /// zamiast FormId
        /// </summary>
        coFormDefinition = 28, // Com--+BD

        /// <summary>
        /// (--+) GRAFCOMName – dla komponentu TDDGraf
        /// </summary>
        coGRAFCOMName = 29,

        /// <summary>
        /// (--+) GRAFPKValue – dla komponentu TDDGraf
        /// </summary>
        coGRAFPKValue = 30,

        /// <summary>
        /// (--+) GRAFFKValue – dla komponentu TDDGraf
        /// </summary>
        coGRAFFKValue = 31,

        /// <summary>
        /// (--+) GRAFAssignFlag – dla komponentu TDDGraf
        /// </summary>
        coGRAFAssignFlag = 32,

        /// <summary>
        /// (--+) GRAFAssignType – dla komponentu TDDGraf
        /// </summary>
        coGRAFAssignType = 33,

        /// <summary>
        /// (--+) GRAFAssignResult – dla komponentu TDDGraf
        /// </summary>
        coGRAFAssignResult = 34,

        /// <summary>
        /// (--+) CancelMessages – przeslanie tego OTHER'a jest równowa¿ne uruchomieniu BDC z parametrem "/CancelMessage"
        /// - czyli w³¹czenie b¹dŸ wy³¹czenie komunikatu informuj¹cego o utracie danych po przerwaniu edycji b¹dŸ
        /// zapisu...
        /// </summary>
        coCancelMessages = 35,

        /// <summary>
        /// (+--+) InformAboutUserDecision – przeslanie tego OTHER'a powoduje wys³anie do komponentu informacji o wyborze
        /// jakiego dokona³ u¿ytkownik podczas ogl¹daniu komunikatu -innymi s³owy jest to wymuszenie na komunikacie
        /// zachowania tzw. ConfirmationResult.Informacja o decyzji u¿ytkownika trafia do komponentu analogicznie jak
        /// przy zwyklym komunikacie ConfirmationResult, tyle ¿e nie w OTTHER'ze 10 a 36. (Ma to zastosowanie podczas
        /// tunelowania komunikatów pochodz¹cych z innych komponentów -np.FormGenerator i komunikaty z plug-in'ów).
        /// UWAGA: OTHER ten ma wiêkszy piorytet anizeli typ komunikatu ConfirmationResult - oznacza to ¿e jezeli
        /// pokazany zosta³ komunikat ConfirmationResult i równoczesnie zostal zwrocony OTHER 36, to BDC zachowa siê jak
        /// opisano przed chwil¹...
        /// </summary>
        coInformAboutUserDecision = 36,

        /// <summary>
        /// (--+) SetGetValueAfterMessage - Ten OTHER doklejany jest zawsze gdy wo³anie metody SetGetValue jest
        /// spowodowane koniecznoœci¹ poinformowania komponentu o decyzji jak¹ podj¹³ u¿ytkownik po zobaczeniu
        /// komunikatu który jest typu ConfirmationResult b¹dŸ dla którego zosta³ przekazany OTHER 36.
        /// </summary>
        coSetGetValueAfterMessage = 37,

        /// <summary>
        /// (--+) ToolBarVisibility - Ten OTHER pozwala sterowaæ widocznoœci¹ ToolBar'ów - podobnie jak ma to miejsce
        /// poprzez paramentr wejœciowy /ToolBar=x
        /// </summary>
        coToolBarVisibility = 38,

        /// <summary>
        /// (--+) ExecuteToolAtClient - Ten OTHER umo¿liwia przekazanie do BDC informacji na temat Tooli(i parametrów ich
        /// uruchomienia) ktore maj¹ zostac uruchomione po analizie wyników metody SetGetValue.Wartoœæ tego OTHERa to
        /// tablica dwuwymiarowa, ktorej pierwsz¹ kolumn¹[0] jest ToolId a drug¹[1] ObjectId.
        /// </summary>
        coExecuteToolAtClient = 39,

        /// <summary>
        /// (--+) SetGetValueOnClose - OTHER ten umo¿liwia wymuszenie wywo³ania SetGetValue podczas zamykania formularza
        /// (Alt-F4 lub X na oknie) -w tym celu nale¿y jednorazowa przeslac w dowolnym momecie tego OTHER'a z wartoscia
        /// "1"("0" deaktywuje koniecznosc wolania metody).Istotne jest, i¿ wolanie metody ma na celu jedynie
        /// poinformowanie komponentu o zamykaniu, NIE SA ANALIZOWANE JEJ WYNIKI CZY ZWROCONE PARAMETRY -formularz siê
        /// zamyka.
        /// </summary>
        coSetGetValueOnClose = 40,

        /// <summary>
        /// (--+) ForceCallSetGetOnExit - Other ten umo¿liwia wymuszenie wywo³ywanie SetGetValue przy wyjsciu z kontrolki
        /// niezalzenie od tego czy wartosc w niej ulegla zmianie czy tez nie - ma to zastosowanie w przypadku, gdy
        /// np.maja byc sprawdzane wartosci w polu i nie mozna wyjsc tak dlugo jak wartosc nie jest prawidlowa(po
        /// wykonaniu SetGetValue i wymuszeniu powrotu do kontroli - znacznik zmiany wartosci zostaje zerowany
        /// i standardowo przy kolejnym wyjsciu nie wystepuje wywolanie SetGetValue)
        /// </summary>
        coForceCallSetGetOnExit = 41,

        /// <summary>
        /// (--+) InformAboutParentClose - Ustawienie wartosci "1" powoduje ¿e jezeli formularz byl uruchomiony z innego
        /// formularza(parent), wowczas przy zamykaniu "parent'a" formularz dostaje informacje o tym w postaci
        /// wywolania metody SetGetValue.Parametr OTHERS : [0,0]= 42[0, 1] = ToolId w wyniku ktorego zostal uruchomiony
        /// parent.
        /// </summary>
        coInformAboutParentClose = 42,

        /// <summary>
        /// (--+) AllowDirectEdit - Other ten umozliwia zaznaczenie ze formularz obsluguje bezposrednie wywolanie
        /// SetGetValue(Edit, ...) w ramach swojej inicjalizacji(od razu pokazanie formularza w edycji, bez
        /// przechodzenia przez View). Ma to zastosowanie w przypadku zlozonych formularzy, gdzie duzo czasu zajmuje
        /// pokazanie trybu View, ktory w tym przypadku jest inorowany.
        /// </summary>
        coAllowDirectEdit = 43,

        /// <summary>
        /// (--+) AddAllFieldsToCallAgain - Other ten umozliwia wymuszenie tego aby przy wolaniu SetGetValue jako
        /// CallAgain byly posylane do komnponentu wartosci wszystkich kontrolek(rowniez typu isView) - ma to
        /// zastosowanie dla DDList, dla ktorych komponent musi sprawdzic wartosc jak na niej jest ustawiana).
        /// </summary>
        coAddAllFieldsToCallAgain = 44,

        /// <summary>
        /// (--+) AddSearchControlToFields - Other ten umo¿liwia wymuszenie przekazywania do FIELDS'ow rowniez wartosci
        /// kontrolek typu isSearch
        /// </summary>
        coAddSearchControlToFields = 45,

        /// <summary>
        /// (--+) AllowNewFormConnectedWithList - Other ten umo¿liwia pod³¹czenie formularza w trybie NEW do zestawienia,
        /// z którego zosta³ uruchomiony(aktywne s¹ przyciski lewo, prawo).
        /// </summary>
        coAllowNewFormConnectedWithList = 46,

        /// <summary>
        /// (+--) DefaultValuesSaveForComponentIn - Other ten wykorzystywany jest do przekazania do komponentów talicy
        /// Fields ze wszystkimi wartosciami kontrolek w celu przygotowania tablicy Fields do zapisania jako szablon.
        /// </summary>
        coDefaultValuesSaveForComponentIn = 47,

        /// <summary>
        /// (--+) DefaultValuesSaveForComponentOut - Other ten umo¿liwia zwrócenie do BDC paczki FIELDS zawiwerajacej dane
        /// maj¹ce zosta zapisane jako szablon.
        /// </summary>
        coDefaultValuesSaveForComponentOut = 48,

        /// <summary>
        /// (+--) DefaultValuesGetForComponent - Other ten umo¿liwia przekazanie do komponentu paczki FIELDS z wartosciami
        /// zapisanymi jako szablon.
        /// </summary>
        coDefaultValuesGetForComponent = 49,

        /// <summary>
        /// (--+) DefaultValuesPossible - Other ten umo¿liwia wl¹czenie obslugi szablonów dla danego formularza
        /// </summary>
        coDefaultValuesPossible = 50,

        /// <summary>
        /// Other ten informuje, ¿e wywolanie metody SetGetValue pochodzi w wyniku nacisniecia przycisku w jednej
        /// z cell kontrolki DDGrid
        /// </summary>
        coButtonClickFromDDGrid = 51,

        /// <summary>
        /// Other ten umo¿liwia przekazanie do formularza nazwy DDButtona, który ma zostaæ "klikniêty" przez formularz
        /// w celu wykonania akcji z nim zwi¹zanej
        /// </summary>
        coClickDDButton = 52,

        /// <summary>
        /// jezeli zostanie zwrocone w SetGetValue, a to SetGetValue by³o wywolane w wyniku rozpoczecia wykonania
        /// procedury zapisu rekordu -wowczas zapis zostanie przerwany(bez ¿adnego komunikatu).Jezeli SetGetValue
        /// by³o wykonane tak sobie - np.gosc wyszedl tabulatorem z pola -wowczas flaga jest ignorowana - czyli jak
        /// goœæ zaraz nacisnie Save - to pojdzie SetGetValue ...
        /// </summary>
        coLockSaveMethod = 53,

        /// <summary>
        /// (--+) Po przekazaniu tego OTHERa przez komponent, BaseDetail sprawdza, czy jego wartoœæ stanowi prawid³owy
        /// identyfikator mandanta, i jeœli tak, to zmienia mandanta dla formularza uwidaczniaj¹c jego wybór w nag³ówku
        /// formularza.
        /// (+--) OTHER ten jest przekazywany do komponentu w momencie wybrania mandanta przez u¿ytkownika ze specjalnego
        /// panelu(patrz opis OTHERa #59). BaseDetail nie zmienia w tym momencie mandanta dla formularza - zrobi to
        /// dopiero po przes³aniu go przez komponent.
        /// Wartoœæ tego OTHERa mo¿e przyj¹æ dwie postacie:
        /// -postaæ prosta: zwyk³y integerowy identyfikator mandanta(przy typie wyboru 1)
        /// - postaæ z³o¿ona: string '/mulItemId=.../mulMandantId=...'(przy typie wyboru 2)
        /// Wybór typu wyboru mandanta jest opisany w opisie OTHERa #59
        /// Typ wyboru nie ma wp³ywu na oczekiwania BaseDetaila co do postaci, w jakiej komponent zwróci identyfikator
        /// mandanta - najpierw nastêpuje próba potraktowania wartoœci jako Integer, a póŸniej(w razie niepowodzenia)
        /// nastêpuje próba odczytania parametru '/mulMandantId='.
        /// </summary>
        coMandantId = 54, // BD+--+Com

        /// <summary>
        /// od wersji 5.7.3.x BaseDetaila ten OTHER nie jest ju¿ u¿ywany
        /// </summary>
        coMandantEnable = 55,

        /// <summary>
        /// ???
        /// </summary>
        coRecordValuesModified = 56,

        /// <summary>
        /// (+--) OTHER ten jest przekazywany tylko w wywo³aniu AssignStartupParameter. Aby odczytaæ numer wersji nale¿y
        /// pos³u¿yæ siê funkcj¹ GetClientVersion() i odczytaæ numer wersji z wVer1..wVer4
        /// </summary>
        coClientVersion = 57, // BD--+Com

        /// <summary>
        /// (--+) OTHER ten s³u¿y do przekazania do BaseDetaila specjalnego tokenu, wed³ug którego bêd¹ nak³adane
        /// negatywne uprawnienia na dany formularz.Jeœli ten token nie zostanie przekazany lub zostanie przekazany
        /// pusty, uprawnienia bêd¹ nak³adane wed³ug identyfikatora narzêdzia, z którego zosta³ uruchomiony formularz.
        /// </summary>
        coBDToken = 58, // Com--+BD

        /// <summary>
        /// (--+) OTHER ten s³u¿y do okreœlenia, czy podczas edycji istniej¹cego lub nowego rekordu na formularzu ma
        /// zostaæ wyœwietlony panel do wyboru mandanta(zawieraj¹cy pole tekstowe i przycisk wyboru) oraz jakiego typu
        /// ma to byæ wybór. Wartoœci to:
        /// &lt; 0 - chowa panel wyboru mandanta
        ///   0 - pokazuje panel i ustawia wybór mandanta na typ 1
        /// &gt; 0 - pokazuje panel i ustawia wybór mandanta na typ 2.Wartoœæ OTHERa jest tu typem tabeli mandantowej
        /// (TableTypeId z tabeli Dictionaries)
        /// </summary>
        coSelectMandant = 59, // Com--+BD

        /// <summary>
        /// (+--) This OTHER is sent to the component only when the form switches to the "NEW RECORD" state.The value
        /// indicates the current mandant, be it the mandant set by the component(in the previous NEW / EDIT state), set
        /// by the parent(invoking) window or the system active mandant - in this order.
        /// </summary>
        coMandantContext = 60, // BD--+Com

        /// <summary>
        /// <para>
        /// (--+) This OTHER may be used to send a custom command to a DD - control or to the client application.The data
        /// is a 2 - dimensional OleVariant array:
        /// </para>
        /// <para>
        /// +-----------+---------+------------+
        /// | FieldName | Command | Parameters |
        /// +-----------+---------+------------+
        /// | FieldName | Command | Parameters |
        /// +-----------+---------+------------+
        /// | ...       | ...     | ...        |
        /// +-----------+---------+------------+
        /// </para>
        /// <para>Command support:</para>
        /// <para>
        /// version 7.0.5.131:
        /// -you can send commands only to a TDDList control, and the supported commands are:
        /// FirstRecord
        /// PreviousRecord
        /// NextRecord
        /// LastRecord
        /// </para>
        /// <para>
        /// version 7.2.4.x:
        /// -you can send commands to the client application(leave the FieldName empty)
        /// -supported command: SendMail(described elsewhere)
        /// </para>
        /// </summary>
        coCustomCommand = 61, // Com--+BD

        /// <summary>
        /// <para>
        /// (--+) This OTHER lets you utilize the global parameters, if any.The data consists of a 2 - dimensional
        /// OleVariant array:
        /// </para>
        /// <para>
        /// +---------+-------+--------+
        /// | ParamId | Value | Symbol |
        /// +---------+-------+--------+
        /// | ParamId | Value | Symbol |
        /// +---------+-------+--------+
        /// | ...     | ...   | ...    |
        /// +---------+-------+--------+
        /// </para>
        /// <para>
        /// UWAGA: Parametry globalne po wczytaniu s¹ dodawane jako Other-y o wartoœciach: coGP1000 + ParamId aby by³
        /// szybszy i ³atwiejszy dostêp do wartoœci parametrów globalnych.Patrz sta³e coGPxxxx!!!
        /// </para>
        /// </summary>
        coGlobalParameters = 62, // BD--+Com

        /// <summary>
        /// (+--) This other lets you set the Clipboard contents on the client machine. The value of this OTHER is a string.
        /// </summary>
        coSetClipboard = 65, // Com--+BD
        /// <summary>
        /// (+--) This other lets you set the caption for any statement spawned from the detail form(be it from DDButton
        /// of btActionMenu or btTool type, from action associated with the current ObjectTypeId, by sending
        /// OTHER#39 etc.)
        /// </summary>
        coStatementCaption = 66, // Com--+BD

        /// <summary>
        /// (--+) Ten Other przekazuje nazwê akcji, która spowodowa³a uruchomienie bie¿acego formularza.
        /// </summary>
        coActionName = 67, // BD--+Com

        /// <summary>
        /// (+--)
        /// </summary>
        coCustomDebugInfo = 68, // Com--+BD

        /// <summary>
        /// (--+) This Other, since version 7.3.39.904, is always passed by the application. 0 means "debug not enabled",
        /// greater than 0 means "debug enabled"
        /// </summary>
        coDebugState = 69, // BD--+Com
        /// <summary>
        /// (--+) This Other, since version xxxxxxxxxxxx, is passed by the application during execution of tool. Value
        /// determines static params used during execution of the tool(defined in tool and Action_ObjectType). Other
        /// is usefull mainly in ToolType = 18(job multi in one) when static params are not passed i other way to
        /// business component.
        /// </summary>
        coToolStaticParams = 70, // BD--+Com

        /// <summary>
        /// This other lets you set information about mandant in caption for form.Remember to pass mulitemid not
        /// mandant's description as a value of this parameter. New version of application will get description by
        /// itself and put it in the Caption.
        /// </summary>
        coMandant = 71, // Com--+BD

        /// <summary>
        /// ???
        /// </summary>
        coDDPanelPreconfig = 72, // Com--+BD

        /// <summary>
        /// This other lets you set information about Main Object Class presented on form.It is an Integer value that
        /// corresponds to ObjectTypeLookupML.ObjectTypeLookupId == ObjectTypeId Application assumes that presented main
        /// object is of the same class like records presented on lookup lists.
        /// In combination with coObjectTypeLookup_ID this information is used to automatically generate Header for
        /// curent form in application. This automatic Header has higher priority then coCaption.
        /// </summary>
        coObjectTypeLookupClass = 73, // Com--+BD

        /// <summary>
        /// This other lets you set a simple ID(Integer) of Main Object presented on form that correspond to the
        /// default Id used to identify object in lookUps.Of course this value should be compatibile with
        /// coObjectTypeLookupClass.
        /// select Lookup_ToolId from ObjectTypeLookUp4App where Lookup_Id = Others[coObjectTypeLookupClass].asInt
        /// ID has to correspond to Id column of Lookup_Txxx views where xxx == Lookup_ToolId
        /// In combination with coObjectTypeLookupClass this information is used to automatically generate Header for
        /// curent form in application.This automatic Header has higher priority then coCaption.
        /// </summary>
        coObjectTypeLookup_ID = 74, // Com--+BD

        /// <summary>
        /// <para>
        /// To umo¿liwia wys³anie do "rodzica"(okna, z którego zosta³ uruchomiony ten formularz) "zdarzenia" podobnego
        /// do wykonania wyjœcia z kontrolki na formularzu.Wartoœci¹ tego othera jest wektor variantowy, gdzie
        /// element[0] zawiera jak¹œ nazwê, a element[1] zawiera wartoœæ. Nazwa nie musi byæ nazw¹ istniej¹cej
        /// kontrolki - mo¿e to byæ cokolwiek(wa¿ne, ¿eby nie by³ to pusty ci¹g).Jeœli komponent zechce wys³aæ w ten
        /// sposób kilka elementów, to mo¿e utworzyæ dwuwymiarow¹ tablicê variantow¹, gdzie w kolumnie[i, 0] bêdzie
        /// nazwa, a w kolumnie[i, 1] -wartoœæ.W takim wypadku do komponentu rodzica zostanie wys³any element
        /// BEZ NAZWY -bo nazwy elementów bêd¹ w tablicy.
        /// W przypadku wys³ania tego othera komponent "rodzica" nie otrzyma wartoœci ActiveFieldName ani
        /// ActiveFieldValue.
        /// </para>
        /// <para>Dostêpne od wersji 1332</para>
        /// </summary>
        coParentField = 75, // Com--+BD

        /// <summary>
        /// <para>
        /// Ten other zawiera to, co inny komponent wys³a³ za pomoc¹ othera #75 - czyli w [0] jest nazwa,
        /// a w[1] -wartoœæ.Jeœli komponent wys³a³ kilka elementów, to w[0] bêdzie PUSTY CI¥G, a w[1] -tablica
        /// z tymi elementami([i, 0] - nazwa, [i, 1] -wartoœæ).
        /// </para>
        /// <para>Dostêpne od wersji 1332</para>
        /// </summary>
        coChildField = 76, // BD--+Com

        /// <summary>
        /// <para>
        /// Ten other nie ma znaczenia w komunikacji z komponentem BaseDetailowym. U¿ywany jest tylko w narzêdziach typu
        /// 8, 9 i 24(czyli tych, co po wykonaniu powoduj¹ odœwie¿enie).
        /// </para>
        /// <para>Dostêpne od wersji 1399.</para>
        /// </summary>
        coForceNoRefresh = 77,

        /// <summary>
        /// <para>
        /// Ten other -jeœli istnieje w paczce - zawiera klucze g³ówne elementów, które wskaza³ ostatnio u¿ytkownik
        /// w kontrolce DDLookupEx.Wskaza³ - czyli wybra³ z listy, albo wybra³ przez szybkie wyszukiwanie.
        /// Other jest w postaci dwuwymiarowej tablicy, gdzie w kolumnie[0] jest integerowy identyfikator rekordu,
        /// a w kolumnie[1] jego stringowy klucz g³ówny(taki, jaki wystêpuje w toolu zestawienia, z którego
        /// wyœwietlana jest lista lookupowa).
        /// </para>
        /// <para>Dostêpny od wersji 1404.</para>
        /// </summary>
        coLastSelectedPrimaryKeys = 78, // BD--+Com

        /// <summary>
        /// Stan kontrolek na formularzu w postaci paczki:[fieldname][dictionaryid][visible][enabled][parentfieldname]
        /// UWAGA: paczka nie jest DBCom-owa czyli nie ma nag³ówków i flag!
        /// Other jest przekazywany tylko gdy Stan formularza zmienia siê na NEW lub EDIT i wczeœniej komponent
        /// wys³a³ Othera coDefaultValuesPossible.
        /// </summary>
        coFieldsDefinition = 79, // BD--+Com

        /// <summary>
        /// Wartoœci domyœlne dla pol pobrane przez EXE i/ lub SC na podstawie coFieldsDefinition i innych informacji.
        /// Wartoœæ ma postaæ paczki FieldsShort gdzie wartoœci to proponowane wartoœci domyœlne.
        /// Mo¿na je za³adowaæ wysterowuj¹c odpowiednio metodê LoadFieldsShort np.:
        /// FBDW.LoadFieldsShort(FBDW.Others[coFieldsDefaults].Value, false, true);
        /// Oczywiœcie Komponent powinien zweryfikowaæ te wartoœci i skopiowaæ je do FieldValue(z DefaultValue)
        /// a nastêpnie zwróciæ do EXE poprzez GetFields(no chyba, ¿e ktoœ od razu za³adowa³ to do FieldValue).
        /// </summary>
        coFieldsDefaults = 80, // BD(SC)--+Com

        /// <summary>
        /// Reserved range for global params values. See coGlobalParameters
        /// </summary>
        coGP1000 = 10000,

        /// <summary>
        /// Firma
        /// </summary>
        coGP_Firm_E0001 = 10001,

        /// <summary>
        /// Rok
        /// </summary>
        coGP_Year_N0001 = 10002,

        /// <summary>
        /// Miesi¹c
        /// </summary>
        coGP_Month_N0002 = 10003,

        /// <summary>
        /// Data
        /// </summary>
        coGP_Date_D0001 = 10004,

        /// <summary>
        /// Reserved range for global params values.
        /// </summary>
        coGP1999 = 19999
    }

    /// <summary>
    /// Class BDField.
    /// </summary>
    public class BDField
    {
        /// <summary>
        /// Properties of the field
        /// </summary>
        public Dictionary<int, object> Properties { get; } = new Dictionary<int, object>();
        /// <summary>
        /// The field name
        /// </summary>
        public string FieldName;
        /// <summary>
        /// The field value
        /// </summary>
        public object FieldValue;

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <returns>System.Object.</returns>
        public object GetProperties()
        {
            object[,] prop;

            if (Properties.Count == 0)
            {
                return null;
            }

            prop = new object[Properties.Count, 2];

            for (int i = 0; i < Properties.Count; i++)
            {
                prop[i, 0] = Properties.ElementAt(i).Key;
                prop[i, 1] = Properties.ElementAt(i).Value;
            }

            return prop;
        }

        /// <summary>
        /// The dictionary
        /// </summary>
        public object Dictionary;

        /// <summary>
        /// Gets as string.
        /// </summary>
        /// <value>As string.</value>
        public string AsString
        {
            get
            {
                return TUniVar.VarToStr(FieldValue);
            }
        }

        /// <summary>
        /// Gets as int.
        /// </summary>
        /// <value>As int.</value>
        public int AsInt
        {
            get
            {
                return TUniVar.VarToInt(FieldValue);
            }
        }

        /// <summary>
        /// Gets as datetime.
        /// </summary>
        /// <value>As datetime.</value>
        public DateTime AsDateTime
        {
            get
            {
                return TUniVar.VarToDateTime(FieldValue, DateTime.FromOADate(TUniConstants._DATE_NULL));
            }
        }

        /// <summary>
        /// Gets as variable.
        /// </summary>
        /// <value>As variable.</value>
        public object AsVar
        {
            get
            {
                return FieldValue;
            }
        }

        /// <summary>
        /// Adds the property.
        /// </summary>
        /// <param name="id">The property identifier.</param>
        /// <param name="PropValue">The property value.</param>
        public void AddModifyProperty(int id, object PropValue)
        {
            if (Properties.ContainsKey(id))
                Properties.Add(id, PropValue);
            else
                Properties[id] = PropValue;
        }
    }

    /// <summary>
    /// Class BDWrapper.
    /// </summary>
    public class BDWrapper
    {
        private readonly StringComparer _comparer = StringComparer.OrdinalIgnoreCase;

        /// <summary>
        /// G³ówny konstruktor klasy
        /// </summary>
        public BDWrapper():base()
        {
            Fields = new Dictionary<string, BDField>(_comparer);
            Others = new Dictionary<TBDOthers, object>();
            Params = new Dictionary<string, object>(_comparer);

            MultiInOneParams = new List<string>();
        }

        /// <summary>
        /// The field list
        /// </summary>
        public Dictionary<string, BDField> Fields { get; }
        /// <summary>
        /// The other list
        /// </summary>
        public Dictionary<TBDOthers, object> Others { get; }
        /// <summary>
        /// The parameter list
        /// </summary>
        public Dictionary<string, object> Params { get; }
        /// <summary>
        /// The MultiInOne parameter list
        /// </summary>
        public List<string> MultiInOneParams { get; }
        /// <summary>
        /// Adds the modify field.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldValue">The field value.</param>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns>BDField.</returns>
        public BDField AddModifyField(string fieldName, object fieldValue, object dictionary)
        {
            if (!Fields.TryGetValue(fieldName, out BDField field))
            {
                field = new BDField
                {
                    FieldName = fieldName
                };
                Fields.Add(fieldName, field);
            }

            field.FieldValue = fieldValue;
            field.Dictionary = dictionary;
            return field;
        }

        /// <summary>
        /// Adds the modify field.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldValue">The field value.</param>
        /// <returns>BDField.</returns>
        public BDField AddModifyField(string fieldName, object fieldValue)
        {
            return AddModifyField(fieldName, fieldValue, null);
        }

        /// <summary>
        /// Adds the modify other.
        /// </summary>
        /// <param name="id">The other identifier.</param>
        /// <param name="value">The other value.</param>
        public void AddModifyOther(TBDOthers id, object value)
        {
            Others[id] = value;
        }

        /// <summary>
        /// Adds the modify parameter.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">The parameter value.</param>
        public void AddModifyParam(string name, object value)
        {
            Params[name] = value;
        }

        /// <summary>
        /// Others the exists.
        /// </summary>
        /// <param name="otherId">The other identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool OtherExists(TBDOthers otherId)
        {
            return Others.ContainsKey(otherId);
        }

        /// <summary>
        /// Fields the exists.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool FieldExists(string fieldName)
        {
            return Fields.ContainsKey(fieldName);
        }

        /// <summary>
        /// Parameters the exists.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ParamExists(string paramName)
        {
            return Params.ContainsKey(paramName);
        }

        /// <summary>
        /// Gets the <see cref="BDField"/> with the specified field name.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>BDField.</returns>
        public BDField this[string fieldName]
        {
            get
            {
                return Fields[fieldName];
            }
        }

        /// <summary>
        /// Deletes the field.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <returns>System.Object.</returns>
        public bool DeleteField(string name)
        {
            return Fields.Remove(name);
        }

        /// <summary>
        /// Deletes the other.
        /// </summary>
        /// <param name="id">The other identifier.</param>
        /// <returns>System.Object.</returns>
        public bool DeleteOther(TBDOthers id)
        {
            return Others.Remove(id);
        }

        /// <summary>
        /// Deletes the parameter.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <returns>System.Object.</returns>
        public bool DeleteParam(string name)
        {
            return Params.Remove(name);
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        public void ClearFields()
        {
            Fields.Clear();
        }

        /// <summary>
        /// Clears the others.
        /// </summary>
        public void ClearOthers()
        {
            Others.Clear();
        }

        /// <summary>
        /// Clears the params.
        /// </summary>
        public void ClearParams()
        {
            Params.Clear();
        }

        /// <summary>
        /// Loads the fields.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <param name="clear">if set to <c>true</c> [clear].</param>
        public void LoadFields(object fields, bool clear = true)
        {
            if (clear)
            {
                ClearFields();
            }

            if (fields == null)
            {
                return;
            }

            object[,] fieldArray = (object[,])fields;

            int count = fieldArray.GetUpperBound(0) + 1;

            for (int i = 0; i < count; i++)
            {
                AddModifyField(fieldArray[i, 0].ToString(), fieldArray[i, 1], fieldArray[i, 2]);
            }
        }

        /// <summary>
        /// Loads the others.
        /// </summary>
        /// <param name="others">The others.</param>
        /// <param name="clear">if set to <c>true</c> [clear].</param>
        public void LoadOthers(object others, bool clear = true)
        {
            if (clear)
            {
                ClearOthers();
            }

            if (others == null)
            {
                return;
            }

            object[,] otherArray = (object[,])others;

            int count = otherArray.GetUpperBound(0) + 1;

            for (int i = 0; i < count; i++)
            {
                AddModifyOther((TBDOthers)otherArray[i, 0], otherArray[i, 1]);
            }
        }

        /// <summary>
        /// Loads the parameters from string.
        /// </summary>
        /// <param name="paramsStr">The parameters string.</param>
        public void LoadParams(string paramsStr)
        {
            string[] separators = new string[] { "/" };
            string[] paramsTable = paramsStr.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            int count = paramsTable.GetUpperBound(0) + 1;

            separators = new string[] { "=" };

            string[] param;

            for (int i = 0; i < count; i++)
            {
                param = paramsTable[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);

                if (param.GetUpperBound(0) > 0)
                    Params[param[0]] = param[1];
                else
                    Params[param[0]] = null;
            }
        }

        /// <summary>
        /// Loads the parameters from table.
        /// </summary>
        /// <param name="params">The parameters string.</param>
        /// <param name="clear">Determines if current parameters list should be cleared before loading new parameters.</param>
        public void LoadParams(object @params, bool clear = true)
        {
            if (clear)
            {
                ClearParams();
            }

            if (@params == null)
            {
                return;
            }

            if (!TUniVar.VarIsArray(@params))
            {
                LoadParams((string)@params);
                return;
            }

            object[,] paramArray = (object[,])@params;

            int count = paramArray.GetUpperBound(0) + 1;

            for (int i = 0; i < count; i++)
            {
                Params[(string)paramArray[i, 0]] = paramArray[i, 1];
            }
        }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <returns>System.Object.</returns>
        public object GetFields()
        {
            object[,] fields = null;

            if (Fields.Count > 0)
            {
                fields = new object[Fields.Count, 4];

                for (int i = 0; i < Fields.Count; i++)
                {
                    fields[i, 0] = Fields.ElementAt(i).Value.FieldName;
                    fields[i, 1] = Fields.ElementAt(i).Value.FieldValue;
                    fields[i, 2] = Fields.ElementAt(i).Value.Dictionary;
                    fields[i, 3] = Fields.ElementAt(i).Value.Properties;
                }
            }

            return fields;
        }

        /// <summary>
        /// Gets the others.
        /// </summary>
        /// <returns>System.Object.</returns>
        public object GetOthers()
        {
            object[,] others = null;

            if (Others.Count > 0)
            {
                others = new object[Others.Count, 2];

                for (int i = 0; i < Others.Count; i++)
                {
                    others[i, 0] = (int)Others.ElementAt(i).Key;
                    others[i, 1] = Others.ElementAt(i).Value;
                }
            }

            return others;
        }

        /// <summary>
        /// Loads the parameters from multi in one param array.
        /// </summary>
        /// <param name="param">The parameters array.</param>
        internal void LoadMultiInOneParams(object param)
        {
            MultiInOneParams.Clear();

            if (param is Array arr)
            {
                if (arr.Rank == 1)
                {
                    foreach (string item in arr)
                    {
                        MultiInOneParams.Add(item);
                    }
                }
                else
                {
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        MultiInOneParams.Add((string)arr.GetValue(i, 0));
                    }
                }
                return;
            }

            string s = param.ToString();

            int idx1 = s.IndexOf('[');
            int idx2 = s.IndexOf(']', idx1 + 1);

            string separator = s.Substring(idx1 + 1, idx2 - idx1 - 1);

            foreach (string item in s.Substring(idx2 + 1)
                                     .Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries))
            {
                MultiInOneParams.Add(item);
            }

        }
    }
}
