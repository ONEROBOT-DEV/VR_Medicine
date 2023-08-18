using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TVP
{
    public class OutlineManager : MonoSinglethon<OutlineManager>
    {

        [SerializeField] MeshOutline _niddle;
        [SerializeField] MeshOutline _adapter;
        [SerializeField] MeshOutline _korzang;
        [SerializeField] MeshOutline _prez;
        [SerializeField] MeshOutline _gel;
        [SerializeField] MeshOutline _genZer;
        [SerializeField] MeshOutline _uzi; 
        [SerializeField] MeshOutline _handR;
        [SerializeField] MeshOutline _handL;
        [SerializeField] MeshOutline _uziDEviceTable;
        [SerializeField] MeshOutline _pompDeviece;
        [SerializeField] MeshOutline _fluid;
        [SerializeField] MeshOutline _helper;
        [SerializeField] MeshOutline _rag;
        [SerializeField] MeshOutline _pedals;

        List<MeshOutline> _list = new List<MeshOutline>();

        public MeshOutline Niddle { get => _niddle; set => _niddle = value; }
        public MeshOutline Adapter { get => _adapter; set => _adapter = value; }
        public MeshOutline Korzang { get => _korzang; set => _korzang = value; }
        public MeshOutline Prez { get => _prez; set => _prez = value; }
        public MeshOutline Gel { get => _gel; set => _gel = value; }
        public MeshOutline GenZer { get => _genZer; set => _genZer = value; }
        public MeshOutline Uzi { get => _uzi; set => _uzi = value; }
        public MeshOutline HandR { get => _handR; set => _handR = value; }
        public MeshOutline HandL { get => _handL; set => _handL = value; }
        public MeshOutline UziDEviceTable { get => _uziDEviceTable; set => _uziDEviceTable = value; }
        public MeshOutline PompDeviece { get => _pompDeviece; set => _pompDeviece = value; }
        public MeshOutline Fluid { get => _fluid; set => _fluid = value; }
        public MeshOutline Helper { get => _helper; set => _helper = value; }
        public MeshOutline Rag { get => _rag; set => _rag = value; }
        public MeshOutline Pedals { get => _pedals; set => _pedals = value; }

        private void Start()
        {
            _list.Add(Niddle);
            _list.Add(Adapter);
            _list.Add(Korzang);
            _list.Add(Prez);
            _list.Add(Gel);
            _list.Add(GenZer);
            _list.Add(Uzi);
            _list.Add(HandR);
            _list.Add(HandL);
            _list.Add(UziDEviceTable);
            _list.Add(PompDeviece);
            _list.Add(Helper);
            _list.Add(Rag);
            _list.Add(Fluid);
            _list.Add(Pedals);
        }

        public void HideAll()
        {
            foreach(MeshOutline model in _list)
            {
                HideModel(model);
            }
        }


        public void  ShowModel(MeshOutline model)
        {
            print(243324);
            model.OutlineWidth = 10;
        }
        public void HideModel(MeshOutline model)
        {
            model.OutlineWidth = 0;
        }
    }
}