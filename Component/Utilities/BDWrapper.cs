// ***********************************************************************
// Assembly         : Component
// Author           : Artur Maciejowski
// Created          : 16-02-2020
//
// Last Modified By : Artur Maciejowski
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="BDWrapper.cs" company="DomConsult Sp. z o.o.">
//     Copyright ©  2021 All rights reserved
// </copyright>
// <summary></summary>
// ***********************************************************************

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
        /// Nazwa exe-ka z jakiego został uruchomiony systemm, np.: granitxp; xeidox; granos
        /// </summary>
        coAppName = -13, // BD->Com!!!
        /// <summary>
        /// Rozdzielczość ekranu na którym jest uruchomiona aplikacja, np.: 1920x1080
        /// </summary>
        coScreenSize = -14, // BD->Com!!!
        /// <summary>
        /// (< -) DesignTime – jeżeli jest i ma wartosc <> 0 to oznacza, że aplikacja pracuje w trybie DesignTime
        /// </summary>
        coDesignTime = -5, // BD->Com!!!
        /// <summary>
        /// 
        /// </summary>
        coObjectId = 1, // Com->BD !!!
        /// <summary>
        /// 
        /// </summary>
        coObjectTypeId = 2, // Com->BD !!!
        /// <summary>
        /// (->) DisabledFunction – jeśli nie ma, to domyślnie przyjmowana jest wartość 0 – brak blokowania funkcji formatki
        /// </summary>
        coDisabledFunction = 3, // Com->BD !!!
        /// <summary>
        /// (->) FormId – jeżeli zostanie zwrócona ta wartość(i będzie różna od FormId obecnie pokazywanej
        /// w BaseDetail’u), wówczas przed przypisaniem wartości do kontrolek, zostanie przebudowany formularz
        /// na BaseDetail’u(nowość!!!!!).
        /// </summary>
        coFormId = 4, // Com->BD !!!
        /// <summary>
        /// (->) ForceReturnAllFieldName – wymusza zwracanie w tablicy Fields(dla zapisu, wycofania zmian oraz 
        ///      aktualizacji kontrolek) wartości wszystkich kontrolek, a nie tylko tych które są modyfikowalne(domyślnie)
        /// </summary>
        coFormCaption = 5, // Com->BD !!!

        coForceReturnAllFieldName = 6, // Com->BD !!!
        /// <summary>
        ///(->) RequiredFieldsArray – wymusza przekazania w parametrze Fields do metody GetSetValues podczas pokazywania
        /// rekordu do podglądu zamiast wartości Unassigned(domyślnie), tablicy z wszystkimi FieldName’ami znalezionymi
        ///na formatce(aby to zadziałało, w parametrze OTHERS z metody AssignStartUpParams musi być przekazane
        ///FormId dla formularza).        
        /// </summary>
        coRequiredFieldsArray = 7, // Com->BD !!!
        /// <summary>
        /// (<-) ActiveControlFieldName – FieldName kontrolki, na której wystapiło zdarzen ie OnExit lub OnEnter lub
        /// FieldName dla którego jest wykonywane zdarzenie po wybraniu elementu na liscie LookUp
        /// </summary>
        coActiveControlFieldName = 8, // BD->Com
        /// <summary>
        /// (< -) ActualizationType – typ zdarzenia które wystapiło na kontrolce(OnExit= 1, OnEnter= 2)
        /// </summary>
        coActualizationType = 9, // BD->Com
        /// <summary>
        /// (< -) ConfirmationResult – zwraca informacje o wyborze użytkownika, który dokonał on po pokazaniu mu
        /// komunikatu wymagajacego informacji zwrotnej do komponentu(Yes = 1, No = 2).
        /// </summary>
        coConfirmationResult = 10, // BD->Com
        /// <summary>
        /// (->) ReturnFieldNameDuringCancel – informacja, że komponent życzy sobie aby podczas Cancelu, otrzymywał
        /// w parametrze Fields wartości zmiennych wprowadzone przez użytkownika
        /// </summary>
        coReturnFieldNameDuringCancel = 11, // Com->BD !!!
        /// <summary>
        /// (->) CallAgain – informacja, że komponent życzy sobie aby jeszcze raz wywołano metodę SetGetValue
        /// </summary>
        coCallAgain = 12, // Com->BD !!!
        /// <summary>
        /// (< -) RefreshAllDictionaries – informacja dla komponentu, że mają zostać odświeżone wszystkie skorowidze w tablicy Fields
        /// </summary>
        coRefreshAllDictionaries = 13, // BD->Com
        /// <summary>
        /// (< -) ActiveControlValue – Jest to wartość klucza głównego zaznaczonego na liście typu LookUp. W przypadku gdy
        /// na DDButton'ie ustawiony jest property MultipleExecute, wówczas na liście LookUp'a możliwa jest
        /// multiselekcja i w tym parametrze zwracana jest zawsze wektor(tablica 1 - wymiarowa) z identyfikatorami
        /// wybranymi na liście.
        /// </summary>
        coActiveControlValue = 14, // BD->Com
        /// <summary>
        /// (->) RebuildForm – Ustawienie tego parametru powoduje wymuszenie przebudowania formularza - ma to
        /// zastosowanie przy DDPanelach, możliwe jest wymuszenie w FormState = 8 ponownego odpytania o liczbę DDPaneli
        /// </summary>
        coRebuildForm = 15, // Com->BD
        /// <summary>
        /// (->) AddAllFieldValuesToArray – Ustawienie tego parametry powoduje wymuszenie wystawiania komponentowi
        /// wypełnionej tablicy Fields(zawartością wszystkich kontrolek) w wołaniu metody SetGetValue z FormState 8
        /// (bez zmian) lub 4096(nacisniecie DDButton'a). Przekazanie tej wartości w jednym OTHERS obowiązuje tak
        /// dlugo, aż nie zostanie odwołane(jeżeli w OTHERS nie pojawia sie dany typ wartości oznacza to że się ona
        /// nie zmienia - przyp.aut.)
        /// </summary>
        coAddAllFieldValuesToArray = 16, // Com->BD
        /// <summary>
        /// (->) AddEditabledFieldValuesToArray – Efekt analogiczy jak dla AddAllFieldValueToArray, z tą różnicą że do
        /// Fields trafiają tylko zawartości pół edytowalnych
        /// </summary>
        coAddEditableFieldValuesToArray = 17, // Com->BD
        /// <summary>
        /// (->) AllowProcessing – Możliwe jest zablokowanie(brak wpisu w OTHERS lub wartosc 0) lub zezwolenie na
        /// uruchomienie(wartosc 1) akcji związanej z naciskanym przyciskiem.
        /// </summary>
        coAllowProcessing = 18, // Com->BD
        /// <summary>
        /// (< -) CancelCallAgain – Informacja, że użytkownik przerwał wykonywanie cyklu CallAgain(wartość nie ma znaczenia).
        /// </summary>
        coCancelCallAgain = 19, // BD->Com
        /// <summary>
        /// (< -) ModificationsCancelled – Informacja, że Update/ Cancel / Delete został przerwany wewnątrz komponentu -
        /// oznacza to że BDC nie będzie wołał ponownie SetGetValue z odpowiednimi flagami właściwymi przerwanej
        /// operacji(oczywiście parametry wynikowe zostaną zanalizowane).
        /// </summary>
        coModificationsCancelled = 20, // BD->Com
        /// <summary>
        /// (->) IgnoreDDButtonsForFIELDSArray – Informacja, że komponent nie chce(wartość TRUE) otrzymywać w tablicy
        /// FIELDS wpisów dotyczących kontrolek DDButton
        /// </summary>
        coIgnoreDDButtonsForFIELDSArray = 21, // Com->BD

        coEnterLikeTab = 22, // Com->BD
        /*
            (->) EnterLikeTab – Wymuszenie, aby naciśnięcie klawisza ENTER w kontrolce formularza powodowało przejście
            do nstępnej kontrolki(podobne zachowania jak naciśnięcie klawisza TAB)
        */
        /// <summary>
        /// (->) FieldNameToActivate – Przekazanie nazwy kontrolki(FieldName), która ma stać się kontrolką aktywną }
        /// </summary>
        coFieldNameToActivate = 23, // Com->BD
        /// <summary>
        /// 
        /// </summary>
        coClientVariable = 24, // Com->BD | BD->Com
        /*
            (< -/->) ClientVariable – Ten OTHER omożliwia przechowanie u klienta dowolnej wartości typu OleVariant.
            Wartość ta będzie dokładana do każdego OTHER'a podczas wołania metody SetGetValue tak długo jak długo będzie
            różna od Unassigned.Ten OTHER ma szczególne wykorzystanie podczas pracy z kilkoma zaznaczonymi rekordami,
            gdy należy przekazywać coś pomiędzy kolejnymi instancjami...
        */
        /// <summary>
        /// (->) ClearAllFields – Wymuszenie wyczyszczenie wszystkich kontrolek na formularzu
        /// </summary>
        coClearAllFields = 25, // Com->BD
        /// <summary>
        /// 
        /// </summary>
        coInParams = 26, // Com->BD
/*
    (->) InParams – parametr pozwalajacy zwrócic ciąg parametrów wejsciowych dla BDC odpowiadający bieżącemu
    stanowi formularza -ma to zastosowanie jeżeli po utworzeniu nowego rekordu, chcemy aby rekord ten dopisal
   się do listy i by można do niego wrócić...
*/
        /// <summary>
        /// 
        /// </summary>
        coForceRefresh = 27, // Com->BD
/*
    (->) ForceRefresh – zwrócenie tego OTHER'a umożliwia wysłanie do rodzica (czyli do aplikacji, z której
    został formularz uruchomiony) informacji, że ma się on odświeżyć.

    Począwszy od wersji 1399 tego othera można użyć również w narzędziu typu 7 aby wymusić odświeżenie.
*/
        /// <summary>
        /// 
        /// </summary>
        coFormDefinition = 28, // Com->BD
        /*
            (->) FormDefinition – poprzez tego OTHER'a można zwrócić do BDC definicje formularza (jako ciąg znaków)
            zamiast FormId
        */
        /// <summary>
        /// (->) GRAFCOMName – dla komponentu TDDGraf
        /// </summary>
        coGRAFCOMName = 29,
        /// <summary>
        /// (->) GRAFPKValue – dla komponentu TDDGraf
        /// </summary>
        coGRAFPKValue = 30,
        /// <summary>
        /// (->) GRAFFKValue – dla komponentu TDDGraf
        /// </summary>
        coGRAFFKValue = 31,
        /// <summary>
        /// (->) GRAFAssignFlag – dla komponentu TDDGraf
        /// </summary>
        coGRAFAssignFlag = 32,
        /// <summary>
        /// (->) GRAFAssignType – dla komponentu TDDGraf
        /// </summary>
        coGRAFAssignType = 33,
        /// <summary>
        /// (->) GRAFAssignResult – dla komponentu TDDGraf
        /// </summary>
        coGRAFAssignResult = 34,
        /// <summary>
        /// 
        /// </summary>
        coCancelMessages = 35,
/*
    (->) CancelMessages – przeslanie tego OTHER'a jest równoważne uruchomieniu BDC z parametrem "/CancelMessage"
     - czyli włączenie bądź wyłączenie komunikatu informującego o utracie danych po przerwaniu edycji bądź
    zapisu...
*/
        /// <summary>
        /// 
        /// </summary>
        coInformAboutUserDecision = 36,
/*
    (<->) InformAboutUserDecision – przeslanie tego OTHER'a powoduje wysłanie do komponentu informacji o wyborze
    jakiego dokonał użytkownik podczas oglądaniu komunikatu -innymi słowy jest to wymuszenie na komunikacie
    zachowania tzw. ConfirmationResult.Informacja o decyzji użytkownika trafia do komponentu analogicznie jak
    przy zwyklym komunikacie ConfirmationResult, tyle że nie w OTTHER'ze 10 a 36. (Ma to zastosowanie podczas
    tunelowania komunikatów pochodzących z innych komponentów -np.FormGenerator i komunikaty z plug-in'ów).
    UWAGA: OTHER ten ma większy piorytet anizeli typ komunikatu ConfirmationResult - oznacza to że jezeli
    pokazany został komunikat ConfirmationResult i równoczesnie zostal zwrocony OTHER 36, to BDC zachowa się jak
    opisano przed chwilą...
*/
        /// <summary>
        /// 
        /// </summary>
        coSetGetValueAfterMessage = 37,
/*
    (->) SetGetValueAfterMessage - Ten OTHER doklejany jest zawsze gdy wołanie metody SetGetValue jest
     spowodowane koniecznością poinformowania komponentu o decyzji jaką podjął użytkownik po zobaczeniu
    komunikatu który jest typu ConfirmationResult bądź dla którego został przekazany OTHER 36.
*/
        /// <summary>
        /// 
        /// </summary>
        coToolBarVisibility = 38,
/*
    (->) ToolBarVisibility - Ten OTHER pozwala sterować widocznością ToolBar'ów - podobnie jak ma to miejsce
    poprzez paramentr wejściowy / ToolBar = x
*/
        /// <summary>
        /// 
        /// </summary>
        coExecuteToolAtClient = 39,
/*
    (->) ExecuteToolAtClient - Ten OTHER umożliwia przekazanie do BDC informacji na temat Tooli(i parametrów ich
     uruchomienia) ktore mają zostac uruchomione po analizie wyników metody SetGetValue.Wartość tego OTHERa to
    tablica dwuwymiarowa, ktorej pierwszą kolumną[0] jest ToolId a drugą[1] ObjectId.
*/
        /// <summary>
        /// 
        /// </summary>
        coSetGetValueOnClose = 40,
/*
    (->) SetGetValueOnClose - OTHER ten umożliwia wymuszenie wywołania SetGetValue podczas zamykania formularza
     (Alt-F4 lub X na oknie) -w tym celu należy jednorazowa przeslac w dowolnym momecie tego OTHER'a z wartoscia
    "1"("0" deaktywuje koniecznosc wolania metody).Istotne jest, iż wolanie metody ma na celu jedynie
  poinformowanie komponentu o zamykaniu, NIE SA ANALIZOWANE JEJ WYNIKI CZY ZWROCONE PARAMETRY -formularz się
    zamyka.
*/
        /// <summary>
        /// 
        /// </summary>
        coForceCallSetGetOnExit = 41,
/*
    (->) ForceCallSetGetOnExit - Other ten umożliwia wymuszenie wywoływanie SetGetValue przy wyjsciu z kontrolki
     niezalzenie od tego czy wartosc w niej ulegla zmianie czy tez nie - ma to zastosowanie w przypadku, gdy
    np.maja byc sprawdzane wartosci w polu i nie mozna wyjsc tak dlugo jak wartosc nie jest prawidlowa(po

   wykonaniu SetGetValue i wymuszeniu powrotu do kontroli - znacznik zmiany wartosci zostaje zerowany
   i standardowo przy kolejnym wyjsciu nie wystepuje wywolanie SetGetValue)
*/
        /// <summary>
        /// 
        /// </summary>
        coInformAboutParentClose = 42,
/*
    (->) InformAboutParentClose - Ustawienie wartosci "1" powoduje że jezeli formularz byl uruchomiony z innego
    formularza(parent), wowczas przy zamykaniu "parent'a" formularz dostaje informacje o tym w postaci
   wywolania metody SetGetValue.Parametr OTHERS : [0,0]= 42[0, 1] = ToolId w wyniku ktorego zostal uruchomiony
    parent.
*/
        /// <summary>
        /// 
        /// </summary>
        coAllowDirectEdit = 43,
/*
    (->) AllowDirectEdit - Other ten umozliwia zaznaczenie ze formularz obsluguje bezposrednie wywolanie
    SetGetValue(Edit, ...) w ramach swojej inicjalizacji(od razu pokazanie formularza w edycji, bez
     przechodzenia przez View). Ma to zastosowanie w przypadku zlozonych formularzy, gdzie duzo czasu zajmuje
    pokazanie trybu View, ktory w tym przypadku jest inorowany.
*/
        /// <summary>
        /// 
        /// </summary>
        coAddAllFieldsToCallAgain = 44,
/*
    (->) AddAllFieldsToCallAgain - Other ten umozliwia wymuszenie tego aby przy wolaniu SetGetValue jako
     CallAgain byly posylane do komnponentu wartosci wszystkich kontrolek(rowniez typu isView) - ma to
    zastosowanie dla DDList, dla ktorych komponent musi sprawdzic wartosc jak na niej jest ustawiana).
*/
        /// <summary>
        /// 
        /// </summary>
        coAddSearchControlToFields = 45,
/*
    (->) AddSearchControlToFields - Other ten umożliwia wymuszenie przekazywania do FIELDS'ow rowniez wartosci
    kontrolek typu isSearch
*/
        /// <summary>
        /// 
        /// </summary>
        coAllowNewFormConnectedWithList = 46,
/*
    (->) AllowNewFormConnectedWithList - Other ten umożliwia podłączenie formularza w trybie NEW do zestawienia,
    z któego został uruchomiony(aktywne są przyciski lewo, prawo).
*/
        /// <summary>
        /// 
        /// </summary>
        coDefaultValuesSaveForComponentIn = 47,
/*
    (< -) DefaultValuesSaveForComponentIn - Other ten wykorzystywany jest do przekazania do komponentów talicy
  
      Fields ze wszystkimi wartosciami kontrolek w celu przygotowania tablicy Fields do zapisania jako szablon.
*/
        /// <summary>
        /// 
        /// </summary>
        coDefaultValuesSaveForComponentOut = 48,
/*
    (->) DefaultValuesSaveForComponentOut - Other ten umożliwia zwrócenie do BDC paczki FIELDS zawiwerajacej dane
    mające zosta zapisane jako szablon.
*/
        /// <summary>
        /// 
        /// </summary>
        coDefaultValuesGetForComponent = 49,
        /*
            (< -) DefaultValuesGetForComponent - Other ten umożliwia przekazanie do komponentu paczki FIELDS z wartosciami
            zapisanymi jako szablon.
        */
        /// <summary>
        /// (->) DefaultValuesPossible - Other ten umożliwia wlączenie obslugi szablonów dla danego formularza
        /// </summary>
        coDefaultValuesPossible = 50,
        /// <summary>
        /// Other ten informuje, że wywolanie metody SetGetValue pochodzi w wyniku nacisniecia przycisku w jednej z cell kontrolki DDGrid
        /// </summary>
        coButtonClickFromDDGrid = 51,
        /// <summary>
        /// Other ten umożliwia przekazanie do formularza nazwy DDButtona, który ma zostać "kliknięty" przez formularz
        /// w celu wykonania akcji z nim związanej
        /// </summary>
        coClickDDButton = 52,
        /// <summary>
        /// 
        /// </summary>
        coLockSaveMethod = 53,
/*
    jezeli zostanie zwrocone w SetGetValue, a to SetGetValue było wywolane w wyniku rozpoczecia wykonania
    procedury zapisu rekordu -wowczas zapis zostanie przerwany(bez żadnego komunikatu).Jezeli SetGetValue
 było wykonane tak sobie - np.gosc wyszedl tabulatorem z pola -wowczas flaga jest ignorowana - czyli jak
gość zaraz nacisnie Save - to pojdzie SetGetValue ...
*/
        /// <summary>
        /// 
        /// </summary>
        coMandantId = 54, // BD<->Com
        /*
            (->) Po przekazaniu tego OTHERa przez komponent, BaseDetail sprawdza, czy jego wartość stanowi prawidłowy
             identyfikator mandanta, i jeśli tak, to zmienia mandanta dla formularza uwidaczniając jego wybór w nagłówku
            formularza.

           (< -) OTHER ten jest przekazywany do komponentu w momencie wybrania mandanta przez użytkownika ze specjalnego
            panelu(patrz opis OTHERa #59). BaseDetail nie zmienia w tym momencie mandanta dla formularza - zrobi to
            dopiero po przesłaniu go przez komponent.

            Wartość tego OTHERa może przyjąć dwie postacie:
            -postać prosta: zwykły integerowy identyfikator mandanta(przy typie wyboru 1)
            - postać złożona: string '/mulItemId=.../mulMandantId=...'(przy typie wyboru 2)

            Wybór typu wyboru mandanta jest opisany w opisie OTHERa #59

            Typ wyboru nie ma wpływu na oczekiwania BaseDetaila co do postaci, w jakiej komponent zwróci identyfikator
            mandanta - najpierw następuje próba potraktowania wartości jako Integer, a później(w razie niepowodzenia)
            następuje próba odczytania parametru '/mulMandantId='.
        */
        /// <summary>
        /// od wersji 5.7.3.x BaseDetaila ten OTHER nie jest już używany
        /// </summary>
        coMandantEnable = 55,
        /// <summary>
        /// 
        /// </summary>
        coRecordValuesModified = 56,
        /// <summary>
        /// 
        /// </summary>
        coClientVersion = 57, // BD->Com
/*
    (< -) OTHER ten jest przekazywany tylko w wywołaniu AssignStartupParameter. Aby odczytać numer wersji należy
      posłużyć się funkcją GetClientVersion() i odczytać numer wersji z wVer1..wVer4
*/
        /// <summary>
        /// 
        /// </summary>
        coBDToken = 58, // Com->BD
/*
    (->) OTHER ten służy do przekazania do BaseDetaila specjalnego tokenu, według którego będą nakładane
    negatywne uprawnienia na dany formularz.Jeśli ten token nie zostanie przekazany lub zostanie przekazany
   pusty, uprawnienia będą nakładane według identyfikatora narzędzia, z którego został uruchomiony formularz.
*/
        /// <summary>
        /// 
        /// </summary>
        coSelectMandant = 59, // Com->BD
/*
    (->) OTHER ten służy do określenia, czy podczas edycji istniejącego lub nowego rekordu na formularzu ma
    zostać wyświetlony panel do wyboru mandanta(zawierający pole tekstowe i przycisk wyboru) oraz jakiego typu
    ma to być wybór. Wartości to:
    < 0 - chowa panel wyboru mandanta
    0 - pokazuje panel i ustawia wybór mandanta na typ 1
  > 0 - pokazuje panel i ustawia wybór mandanta na typ 2.Wartość OTHERa jest tu typem tabeli mandantowej
       (TableTypeId z tabeli Dictionaries)
*/
        /// <summary>
        /// 
        /// </summary>
        coMandantContext = 60, // BD->Com
/*
    (< -) This OTHER is sent to the component only when the form switches to the "NEW RECORD" state.The value
    indicates the current mandant, be it the mandant set by the component(in the previous NEW / EDIT state), set
     by the parent(invoking) window or the system active mandant - in this order.
*/
        /// <summary>
        /// 
        /// </summary>
        coCustomCommand = 61, // Com->BD
/*
    (->) This OTHER may be used to send a custom command to a DD - control or to the client application.The data
    is a 2 - dimensional OleVariant array:

    +-----------+---------+------------+
    | FieldName | Command | Parameters |
    +-----------+---------+------------+
    | FieldName | Command | Parameters |
    +-----------+---------+------------+
    | ...       | ...     | ...        |
    +-----------+---------+------------+

    Command support:

    version 7.0.5.131:
        -you can send commands only to a TDDList control, and the supported commands are:
          FirstRecord
          PreviousRecord
          NextRecord
          LastRecord

    version 7.2.4.x:
        -you can send commands to the client application(leave the FieldName empty)
       - supported command: SendMail(described elsewhere)
*/
        /// <summary>
        /// 
        /// </summary>
        coGlobalParameters = 62, // BD->Com
        /*
            (->) This OTHER lets you utilize the global parameters, if any.The data consists of a 2 - dimensional
            OleVariant array:

            +---------+-------+--------+
            | ParamId | Value | Symbol |
            +---------+-------+--------+
            | ParamId | Value | Symbol |
            +---------+-------+--------+
            | ...     | ...   | ...    |
            +---------+-------+--------+

            UWAGA: Parametry globalne po wczytaniu są dodawane jako Other-y o wartościach: coGP1000 + ParamId aby był
            szybszy i łatwiejszy dostęp do wartości parametrów globalnych.Patrz stałe coGPxxxx!!!
        */
        /// <summary>
        /// (< -) This other lets you set the Clipboard contents on the client machine. The value of this OTHER is a string.
        /// </summary>
        coSetClipboard = 65, // Com->BD
        /// <summary>
        /// 
        /// </summary>
        coStatementCaption = 66, // Com->BD
        /*
            (< -) This other lets you set the caption for any statement spawned from the detail form(be it from DDButton
             of btActionMenu or btTool type, from action associated with the current ObjectTypeId, by sending

              OTHER#39 etc.)
        */
        /// <summary>
        /// (->) Ten Other przekazuje nazwę akcji, która spowodowała uruchomienie bieżacego formularza.
        /// </summary>
        coActionName = 67, // BD->Com
        /// <summary>
        /// (< -)
        /// </summary>
        coCustomDebugInfo = 68, // Com->BD
        /// <summary>
        /// (->) This Other, since version 7.3.39.904, is always passed by the application. 0 means "debug not enabled",
        /// greater than 0 means "debug enabled"
        /// </summary>
        coDebugState = 69, // BD->Com
        /// <summary>
        /// 
        /// </summary>
        coToolStaticParams = 70, // BD->Com
/*
    (->) This Other, since version xxxxxxxxxxxx, is passed by the application during execution of tool. Value
     determines static params used during execution of the tool(defined in tool and Action_ObjectType). Other
    is usefull mainly in ToolType = 18(job multi in one) when static params are not passed i other way to
     business component.
*/
        /// <summary>
        /// 
        /// </summary>
        coMandant = 71, // Com->BD
/*
    This other lets you set information about mandant in caption for form.Remember to pass mulitemid not
    mandant's description as a value of this parameter. New version of application will get description by
    itself and put it in the Caption.
*/
        /// <summary>
        /// 
        /// </summary>
        coDDPanelPreconfig = 72, // Com->BD
        /// <summary>
        /// 
        /// </summary>
        coObjectTypeLookupClass = 73, // Com->BD
/*
    This other lets you set information about Main Object Class presented on form.It is an Integer value that
    corresponds to ObjectTypeLookupML.ObjectTypeLookupId == ObjectTypeId Application assumes that presented main
    object is of the same class like records presented on lookup lists.

    In combination with coObjectTypeLookup_ID this information is used to automatically generate Header for
    curent form in application. This automatic Header has higher priority then coCaption.
*/
        /// <summary>
        /// 
        /// </summary>
        coObjectTypeLookup_ID = 74, // Com->BD
/*
    This other lets you set a simple ID(Integer) of Main Object presented on form that correspond to the
    default Id used to identify object in lookUps.Of course this value should be compatibile with
    coObjectTypeLookupClass.

    select Lookup_ToolId from ObjectTypeLookUp4App where Lookup_Id = Others[coObjectTypeLookupClass].asInt

    ID has to correspond to Id column of Lookup_Txxx views where xxx == Lookup_ToolId

    In combination with coObjectTypeLookupClass this information is used to automatically generate Header for
    curent form in application.This automatic Header has higher priority then coCaption.
*/
        /// <summary>
        /// 
        /// </summary>
        coParentField = 75, // Com->BD
/*
    To umożliwia wysłanie do "rodzica"(okna, z którego został uruchomiony ten formularz) "zdarzenia" podobnego
    do wykonania wyjścia z kontrolki na formularzu.Wartością tego othera jest wektor variantowy, gdzie
    element[0] zawiera jakąś nazwę, a element[1] zawiera wartość. Nazwa nie musi być nazwą istniejącej
    kontrolki - może to być cokolwiek(ważne, żeby nie był to pusty ciąg).Jeśli komponent zechce wysłać w ten
  sposób kilka elementów, to może utworzyć dwuwymiarową tablicę variantową, gdzie w kolumnie[i, 0] będzie
 nazwa, a w kolumnie[i, 1] -wartość.W takim wypadku do komponentu rodzica zostanie wysłany element
    BEZ NAZWY -bo nazwy elementów będą w tablicy.

    W przypadku wysłania tego othera komponent "rodzica" nie otrzyma wartości ActiveFieldName ani
    ActiveFieldValue.

    Dostępne od wersji 1332
*/
        /// <summary>
        /// 
        /// </summary>
        coChildField = 76, // BD->Com
/*
    Ten other zawiera to, co inny komponent wysłał za pomocą othera #75 - czyli w [0] jest nazwa,
    a w[1] -wartość.Jeśli komponent wysłał kilka elementów, to w[0] będzie PUSTY CIĄG, a w[1] -tablica
    z tymi elementami([i, 0] - nazwa, [i, 1] -wartość).

    Dostępne od wersji 1332
*/
        /// <summary>
        /// 
        /// </summary>
        coForceNoRefresh = 77,
/*
    Ten other nie ma znaczenia w komunikacji z komponentem BaseDetailowym. Używany jest tylko w narzędziach typu
    8, 9 i 24(czyli tych, co po wykonaniu powodują odświeżenie).

    Dostępne od wersji 1399.
*/
        /// <summary>
        /// 
        /// </summary>
        coLastSelectedPrimaryKeys = 78, // BD->Com
/*
    Ten other -jeśli istnieje w paczce - zawiera klucze główne elementów, które wskazał ostatnio użytkownik
    w kontrolce DDLookupEx.Wskazał - czyli wybrał z listy, albo wybrał przez szybkie wyszukiwanie.

    Other jest w postaci dwuwymiarowej tablicy, gdzie w kolumnie[0] jest integerowy identyfikator rekordu,
   a w kolumnie[1] jego stringowy klucz główny(taki, jaki występuje w toolu zestawienia, z którego

  wyświetlana jest lista lookupowa).

    Dostępny od wersji 1404.
*/
        /// <summary>
        /// 
        /// </summary>
        coFieldsDefinition = 79, // BD->Com
/*
    Stan kontrolek na formularzu w postaci paczki:[fieldname][dictionaryid][visible][enabled][parentfieldname]
UWAGA: paczka nie jest DBCom-owa czyli nie ma nagłówków i flag!
Other jest przekazywany tylko gdy Stan formularza zmienia się na NEW lub EDIT i wcześniej komponent
wysłał Othera coDefaultValuesPossible.
*/
        /// <summary>
        /// 
        /// </summary>
        coFieldsDefaults = 80, // BD(SC)->Com
        /*
            Wartości domyślne dla pol pobrane przez EXE i/ lub SC na podstawie coFieldsDefinition i innych informacji.
            Wartość ma postać paczki FieldsShort gdzie wartości to proponowane wartości domyślne.
            Można je załadować wysterowując odpowiednio metodę LoadFieldsShort np.:
              FBDW.LoadFieldsShort(FBDW.Others[coFieldsDefaults].Value, false, true);
            Oczywiście Komponent powinien zweryfikować te wartości i skopiować je do FieldValue(z DefaultValue)
            a następnie zwrócić do EXE poprzez GetFields(no chyba, że ktoś od razu załadował to do FieldValue).  
        */
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
        /// Miesiąc
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
        public Dictionary<int, object> Properties { get; private set; }  = new Dictionary<int, object>();
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

        public BDWrapper():base()
        {
            Fields = new Dictionary<string, BDField>(_comparer);
            Others = new Dictionary<TBDOthers, object>();
            Params = new Dictionary<string, object>(_comparer);
        }

        /// <summary>
        /// The field list
        /// </summary>
        public Dictionary<string, BDField> Fields { get; private set; }
        /// <summary>
        /// The other list
        /// </summary>
        public Dictionary<TBDOthers, object> Others { get; private set; }
        /// <summary>
        /// The parameter list
        /// </summary>
        public Dictionary<string, object> Params { get; private set; }
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
            if (Others.ContainsKey(id))
                Others[id] = value;
            else
                Others.Add(id, value);
        }

        /// <summary>
        /// Adds the modify parameter.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">The parameter value.</param>
        public void AddModifyParam(string name, object value)
        {
            if (Params.ContainsKey(name))
                Params[name] = value;
            else Params.Add(name, value);
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
        /// Loads the parameters.
        /// </summary>
        /// <param name="ParamsStr">The parameters string.</param>
        public void LoadParams(string ParamsStr)
        {
            string[] separators = new string[] { "/" };
            string[] paramsTable = ParamsStr.Split(separators, StringSplitOptions.RemoveEmptyEntries);

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
            throw new NotImplementedException();
        }
    }
}
